namespace BusTickets.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Station : EntityBase
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Trip> TripOriginStations { get; set; } = new List<Trip>();

        public virtual ICollection<Trip> TripDestinationStations { get; set; } = new List<Trip>();
    }
}
