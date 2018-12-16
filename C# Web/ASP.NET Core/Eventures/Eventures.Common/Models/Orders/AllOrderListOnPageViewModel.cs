namespace Eventures.Common.Models.Orders
{
    using System.Collections.Generic;

    public class AllOrderListOnPageViewModel
    {
        public IEnumerable<AllOrderListViewModel> Orders { get; set; }
    }
}
