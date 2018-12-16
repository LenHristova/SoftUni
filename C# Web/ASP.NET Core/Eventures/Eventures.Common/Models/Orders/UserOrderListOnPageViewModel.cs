namespace Eventures.Common.Models.Orders
{
    using System.Collections.Generic;

    public class UserOrderListOnPageViewModel
    {
        public IEnumerable<UserOrderListViewModel> Events { get; set; }
    }
}
