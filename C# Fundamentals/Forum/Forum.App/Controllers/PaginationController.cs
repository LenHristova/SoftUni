namespace Forum.App.Controllers
{
    public class PaginationController
    {
        public PaginationController(int pageOffset)
        {
            PageOffset = pageOffset;
            CurrentPage = 0;
            AllPagesLinesCount = 0;
        }

        public int PageOffset { get; set; }

        public int CurrentPage { get; set; }

        public int AllPagesLinesCount { get; set; }

        public int LastPage => this.AllPagesLinesCount / (this.PageOffset + 1);

        public bool IsFirstPage => this.CurrentPage == 0;

        public bool IsLastPage => this.CurrentPage == this.LastPage;

        public void ChangePage(bool forward = true)
        {
            this.CurrentPage += forward ? 1 : -1;
        }

        public int PreviousPagesLinesCount()
        {
            return this.CurrentPage * PageOffset;
        }
    }
}
