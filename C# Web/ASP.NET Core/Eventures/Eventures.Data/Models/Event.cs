namespace Eventures.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Event : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
