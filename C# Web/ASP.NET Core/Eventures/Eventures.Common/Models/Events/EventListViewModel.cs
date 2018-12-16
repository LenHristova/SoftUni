namespace Eventures.Common.Models.Events
{
    using System;
    using System.Globalization;

    public class EventListViewModel
    {
        public int Number { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string StartToString
            => this.Start.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture);

        public string EndToString
            => this.End.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture);

        public int TotalTickets { get; set; }
    }
}
