namespace Eventures.Services
{
    using Common.Constants;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using X.PagedList;

    public class PaginationService : IPaginationService
    {
        public IPagedList<TItem> Paginate<TItem>(int page, IEnumerable<TItem> items)
            => items.ToPagedList(page, GlobalConstants.EventsCountOnPage);

        public int ValidatePage(int? page, double itemsOnPage)
        {
            var pagesCount = Math.Ceiling(itemsOnPage / GlobalConstants.EventsCountOnPage);
            return this.IsValidPage(page, pagesCount) ? page.Value : 1;
        }

        private bool IsValidPage(int? page, double pagesCount)
            => page.HasValue && page.Value > 1 && page.Value <= pagesCount;
    }
}
