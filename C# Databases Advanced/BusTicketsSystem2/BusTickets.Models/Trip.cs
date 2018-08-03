namespace BusTickets.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Trip : EntityBase
    {
        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public TripStatus Status { get; set; }

        public int OriginStationId { get; set; }
        public virtual Station OriginStation { get; set; }

        public int DestinationStationId { get; set; }
        public virtual Station DestinationStation { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
