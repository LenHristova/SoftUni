namespace CarDealer.Web.Models
{
    using Infrastructure;
    using System;

    public class Pagination
    {
        public Pagination(int currentPage, int objectsCount)
        {
            this.CurrentPage = currentPage;
            this.ObjectsCount = objectsCount;
        }

        public int CurrentPage { get; set; }

        public int ObjectsCount { get; set; }

        public int MaxPageCountToShow
            => Math.Min(Constants.MaxPageCountToShow, this.TotalPages);

        public int TotalPages =>
            (int)Math.Ceiling((double)this.ObjectsCount / Constants.PageOffset);

        public int PreviousPage
            => this.CurrentPage == 1
                ? 1
                : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;

        public int StartPoint
            => Math.Min(
                Math.Max(this.CurrentPage - (this.MaxPageCountToShow + 1) / 2, 0), 
                this.TotalPages - this.MaxPageCountToShow);
    }
}
