namespace Forum.App.Controllers
{
    using Forum.App.Services;
    using Forum.App.UserInterface;
    using Forum.App.Views;
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;

    public class PostDetailsController : IController, IUserRestrictedController
    {
        public bool LoggedInUser { get; private set; }

        public int PostId { get; private set; }

        public MenuState ExecuteCommand(int index)
        {
            switch ((Command)index)
            {
                case Command.AddReply:
                    return MenuState.AddReplyToPost;
                case Command.Back:
                    ForumViewEngine.ResetBuffer();
                    return MenuState.Back;
            }

            throw new InvalidCommandException();
        }

        public IView GetView(string userName)
        {
            var postViewModel = PostService.GetPostViewModel(this.PostId);
            return new PostDetailsView(postViewModel, this.LoggedInUser);
        }

        public void SetPostId(int postId)
        {
            this.PostId = postId;
        }

        public void UserLogIn()
        {
            this.LoggedInUser = true;
        }

        public void UserLogOut()
        {
            this.LoggedInUser = false;
        }

        private enum Command
        {
            Back,
            AddReply
        }
    }
}
