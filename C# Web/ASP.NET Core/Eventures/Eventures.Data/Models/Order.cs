namespace Eventures.Data.Models
{
    using System;

    public class Order : BaseEntity<string>
    {
        public DateTime OrderedOn { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string CustomerId { get; set; }
        public User Customer { get; set; }

        public int TicketsCount { get; set; }
    }
}
