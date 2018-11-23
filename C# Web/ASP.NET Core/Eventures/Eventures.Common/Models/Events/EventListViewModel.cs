namespace Eventures.Common.Models.Events
{
    using System;

    public class EventListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime End { get; set; }

        public string StartToString { get; set; }

        public string EndToString { get; set; }

        public int TotalTickets { get; set; }
    }
}
