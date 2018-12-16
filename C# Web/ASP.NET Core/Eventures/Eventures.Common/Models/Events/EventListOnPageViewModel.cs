namespace Eventures.Common.Models.Events
{
    using System.Collections.Generic;

    public class EventListOnPageViewModel
    {
        public IEnumerable<EventListViewModel> Events { get; set; }
    }
}
