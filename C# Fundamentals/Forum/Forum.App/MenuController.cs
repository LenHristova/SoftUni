namespace Forum.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Forum.App.Controllers;
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface;
    using Forum.App.UserInterface.Contracts;
    using Forum.App.Controllers.CategoryControllers;
    using Forum.App.Services;

    internal class MenuController
    {
        private const int DEFAULT_INDEX = 0;

        private IController[] controllers;
        private Stack<int> controllerHistory;
        private int currentOptionIndex;
        private ForumViewEngine forumViewer;

        public MenuController(IEnumerable<IController> controllers, ForumViewEngine forumViewer)
        {
            this.controllers = controllers.ToArray();
            this.forumViewer = forumViewer;

            InitializeControllerHistory();

            this.currentOptionIndex = DEFAULT_INDEX;
        }

        private string Username { get; set; }
        private IView CurrentView { get; set; }

        private MenuState State => (MenuState)controllerHistory.Peek();
        //private int CurrentControllerIndex => this.controllerHistory.Peek();
        private IController CurrentController => this.controllers[this.controllerHistory.Peek()];
        internal ILabel CurrentLabel => this.CurrentView.Buttons[currentOptionIndex];

        private void InitializeControllerHistory()
        {
            if (controllerHistory != null)
            {
                throw new InvalidOperationException($"{nameof(controllerHistory)} already initialized!");
            }

            const int mainControllerIndex = 0;
            this.controllerHistory = new Stack<int>();
            this.controllerHistory.Push(mainControllerIndex);
            this.RenderCurrentView();
        }

        internal void PreviousOption()
        {
            this.currentOptionIndex--;

            if (this.currentOptionIndex < 0)
            {
                this.currentOptionIndex += this.CurrentView.Buttons.Length;
            }

            if (this.CurrentLabel.IsHidden)
            {
                this.PreviousOption();
            }
        }

        internal void NextOption()
        {
            this.currentOptionIndex++;

            int totalOptions = this.CurrentView.Buttons.Length;

            if (this.currentOptionIndex >= totalOptions)
            {
                this.currentOptionIndex -= totalOptions;
            }

            if (this.CurrentLabel.IsHidden)
            {
                this.NextOption();
            }
        }

        internal void Back()
        {
            if (this.State == MenuState.Categories || this.State == MenuState.OpenCategory)
            {
                IPaginationableController currentController = (IPaginationableController)this.CurrentController;
                currentController.PaginationController.CurrentPage = 0;
            }

            if (controllerHistory.Count > 1)
            {
                controllerHistory.Pop();
                this.currentOptionIndex = DEFAULT_INDEX;
            }
            RenderCurrentView();
        }

        internal void ExecuteCommand()
        {
            MenuState newState = this.CurrentController.ExecuteCommand(currentOptionIndex);
            switch (newState)
            {
                case MenuState.PostAdded:
                    AddPost();
                    break;
                case MenuState.OpenCategory:
                    OpenCategory();
                    break;
                case MenuState.ViewPost:
                    ViewPost();
                    break;
                case MenuState.SuccessfulLogIn:
                    SuccessfulLogin();
                    break;
                case MenuState.LoggedOut:
                    LogOut();
                    break;
                case MenuState.Back:
                    this.Back();
                    break;
                case MenuState.Error:
                case MenuState.Rerender:
                    RenderCurrentView();
                    break;
                case MenuState.AddReplyToPost:
                    RedirectToAddReply();
                    break;
                case MenuState.ReplyAdded:
                    AddReply();
                    break;
                default:
                    this.RedirectToMenu(newState);
                    break;
            }
        }

        private void AddReply()
        {
            this.Back();
        }

        private void RedirectToAddReply()
        {

            var postDetailsController = (PostDetailsController)this.CurrentController;

            var addReplyController = (AddReplyController)this.controllers[(int)MenuState.AddReply];
            addReplyController.SetPostId(postDetailsController.PostId);
         
            this.RedirectToMenu(MenuState.AddReply);
        }

        private void LogOut()
        {
            this.Username = string.Empty;
            this.LogOutUser();
            this.RenderCurrentView();
        }

        private void SuccessfulLogin()
        {
            var loginController = (IReadUserInfoController)this.CurrentController;
            this.Username = loginController.Username;

            this.LogInUser();
            this.RedirectToMenu(MenuState.Main);
        }

        private void ViewPost()
        {
            var categoryController = (CategoryController)this.CurrentController;
            var categoryId = categoryController.CategoryId;

            var posts = PostService.GetPostsByCategory(categoryId).ToArray();
            var postIndex = categoryController.PaginationController.CurrentPage *
                            categoryController.PaginationController.PageOffset +
                            this.currentOptionIndex;
            var postId = posts[postIndex - 1].Id;

            var postController = (PostDetailsController)this.controllers[(int)MenuState.ViewPost];
            postController.SetPostId(postId);

            this.RedirectToMenu(MenuState.ViewPost);
        }

        private void OpenCategory()
        {
            var categoriesController = (CategoriesController)this.CurrentController;

            var categoryIndex = categoriesController.PaginationController.CurrentPage *
                                categoriesController.PaginationController.PageOffset +
                                this.currentOptionIndex;

            var categoryController = (CategoryController)this.controllers[(int)MenuState.OpenCategory];
            categoryController.SetCategory(categoryIndex);

            this.RedirectToMenu(MenuState.OpenCategory);
        }

        private void AddPost()
        {
            var addPostController = (AddPostController)this.CurrentController;

            var postId = addPostController.Post.PostId;

            var postViewer = (PostDetailsController)this.controllers[(int)MenuState.ViewPost];
            postViewer.SetPostId(postId);

            addPostController.ResetPost();

            this.controllerHistory.Pop();
            this.RedirectToMenu(MenuState.ViewPost);
        }

        private void RenderCurrentView()
        {
            this.CurrentView = this.CurrentController.GetView(this.Username);
            this.currentOptionIndex = DEFAULT_INDEX;
            this.forumViewer.RenderView(this.CurrentView);
        }

        private bool RedirectToMenu(MenuState newState)
        {
            if (this.State == newState)
            {
                return false;
            }

            this.controllerHistory.Push((int)newState);
            this.RenderCurrentView();
            return true;
        }

        private void LogInUser()
        {
            foreach (var controller in this.controllers)
            {
                if (controller is IUserRestrictedController userRestrictedController)
                {
                    userRestrictedController.UserLogIn();
                }
            }
        }

        private void LogOutUser()
        {
            foreach (var controller in this.controllers)
            {
                if (controller is IUserRestrictedController userRestrictedController)
                {
                    userRestrictedController.UserLogOut();
                }
            }
        }
    }
}