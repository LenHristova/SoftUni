namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Trip
    {
        //•	Id – integer, Primary Key
        //•	OriginStationId – integer(required)
        //•	OriginStation – station from which the trip begins(required)
        //•	DestinationStation Id – integer(required)
        //•	DestinationStation –  station where the trip ends(required)
        //•	DepartureTime – date and time of departure from origin station(required)
        //•	ArrivalTime – date and time of arrival at destination station, must be after departure time(required)
        //•	TrainId – integer(required)
        //•	Train – train used for that particular trip(required)
        //•	Status – TripStatus enumeration with possible values: "OnTime", "Delayed" and "Early" (default: "OnTime")
        //•	TimeDifference – time(span) representing how late or early a given train was(optional)

        public int Id { get; set; }

        [Required]
        public int OriginStationId { get; set; }
        [Required]
        public virtual Station OriginStation { get; set; }

        [Required]
        public int DestinationStationId { get; set; }
        [Required]
        public virtual Station DestinationStation { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        //must be after departure time
        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int TrainId { get; set; }
        [Required]
        public virtual Train Train { get; set; }

        public TripStatus Status { get; set; } = TripStatus.OnTime;

        public TimeSpan? TimeDifference { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
