namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Train
    {
        //•	Id – integer, Primary Key
        //•	TrainNumber – text with max length 10 (required, unique) 
        //•	Type – TrainType enumeration with possible values: "HighSpeed", "LongDistance" or "Freight" (optional)
        //•	TrainSeats – Collection of type TrainSeat
        //•	Trips – Collection of type Trip


        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        //unique
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; } = new List<TrainSeat>();

        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
