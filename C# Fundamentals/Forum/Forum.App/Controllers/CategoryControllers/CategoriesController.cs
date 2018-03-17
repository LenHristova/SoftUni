namespace Forum.App.Controllers.CategoryControllers
{
    using System.Linq;

    using Forum.App.Controllers.Contracts;
    using Forum.App.Services;
    using Forum.App.UserInterface.Contracts;
    using Forum.App.Views;

    public class CategoriesController : IController, IPaginationableController
    {
        private const int PAGE_OFFSET = 10;

        public CategoriesController()
        {
            PaginationController = new PaginationController(PAGE_OFFSET);
            this.LoadCategories();
        }

        private string[] AllCategoryNames { get; set; }

        private string[] CurrentPageCategories { get; set; }

        public PaginationController PaginationController { get; }

        private void LoadCategories()
        {
            this.AllCategoryNames = PostService.GetAllGategoryNames().ToArray();

            PaginationController.AllPagesLinesCount = AllCategoryNames.Length;

            this.CurrentPageCategories = this.AllCategoryNames
                .Skip(PaginationController.PreviousPagesLinesCount())
                .Take(PaginationController.PageOffset)
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
                case Command.ViewCategory:
                    return MenuState.OpenCategory;
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
            this.LoadCategories();
            return new CategoriesView(this.CurrentPageCategories, PaginationController.IsFirstPage, PaginationController.IsLastPage);
        }

        private enum Command
        {
            Back = 0,
            ViewCategory = 1,
            PreviousPage = 11,
            NextPage = 12
        }
    }
}
