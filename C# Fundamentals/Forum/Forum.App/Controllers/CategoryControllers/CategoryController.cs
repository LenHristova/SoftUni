namespace Forum.App.Controllers.CategoryControllers
{
    using System.Linq;

    using Forum.App.Controllers.Contracts;
    using Forum.App.Services;
    using Forum.App.UserInterface.Contracts;
    using Forum.App.Views;
    using Forum.Models.Contracts;

    public class CategoryController : IController, IPaginationableController
    {
        private const int PAGE_OFFSET = 10;

        public CategoryController()
        {
            PaginationController = new PaginationController(PAGE_OFFSET);
            this.SetCategory(0);
        }
        private IPost[] AllPostTitles { get; set; }

        private string[] CurrentPageTitles { get; set; }

        public int CategoryId { get; private set; }

        public PaginationController PaginationController { get; }

        public void SetCategory(int categoryId)
        {
            this.CategoryId = categoryId;
        }

        public void GetPosts()
        {
            this.AllPostTitles = PostService.GetPostsByCategory(this.CategoryId)
                .ToArray();

            PaginationController.AllPagesLinesCount = AllPostTitles.Length;

            this.CurrentPageTitles = this.AllPostTitles
                            .Skip(this.PaginationController.PreviousPagesLinesCount())
                            .Take(PaginationController.PageOffset)
                .Select(p => p.Title)
                            .ToArray();
        }

        public MenuState ExecuteCommand(int index)
        {
            if (index > 1 && index < 11)
            {
                index = 1;
            }

            switch ((Command)index)
            {
                case Command.Back:
                    return MenuState.Back;
                case Command.ViewPost:
                    return MenuState.ViewPost;
                case Command.PreviousPage:
                    PaginationController.ChangePage(false);
                    return MenuState.Rerender;
                case Command.NextPage:
                    PaginationController.ChangePage();
                    return MenuState.Rerender;
            }

            throw new InvalidCommandException();
        }

        public IView GetView(string userName)
        {
            this.GetPosts();
            var categoryName = PostService.GetCategory(this.CategoryId).Name;
            return new CategoryView(categoryName, this.CurrentPageTitles, PaginationController.IsFirstPage, PaginationController.IsLastPage);
        }

        protected enum Command
        {
            Back = 0,
            ViewPost = 1,
            PreviousPage = 11,
            NextPage = 12
        }
    }
}
