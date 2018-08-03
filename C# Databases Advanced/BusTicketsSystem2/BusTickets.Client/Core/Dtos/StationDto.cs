namespace BusTickets.Client.Core.Dtos
{
    using System.Collections.Generic;
    using Models;

    public class StationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TownBaseDto Town { get; set; }

        public virtual ICollection<TripDto> TripOriginStations { get; set; } 

        public virtual ICollection<TripDto> TripDestinationStations { get; set; } 
    }
}
