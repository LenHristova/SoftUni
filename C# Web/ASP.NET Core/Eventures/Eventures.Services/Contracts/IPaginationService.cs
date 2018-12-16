namespace Eventures.Services.Contracts
{
    using System.Collections.Generic;
    using X.PagedList;

    public interface IPaginationService
    {
        IPagedList<TItem> Paginate<TItem>(int page, IEnumerable<TItem> items);

        int ValidatePage(int? page, double itemsOnPage);
    }
}
