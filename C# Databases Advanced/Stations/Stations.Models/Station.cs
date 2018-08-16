namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Station
    {
        //•	Id – integer, Primary Key
        //•	Name – text with max length 50 (required, unique)
        //•	Town – text with max length 50 (required)
        //•	TripsTo – Collection of type Trip
        //•	TripsFrom – Collection of type Trip

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        //unique
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Town { get; set; }

        public virtual ICollection<Trip> TripsTo { get; set; } = new List<Trip>();

        public virtual ICollection<Trip> TripsFrom { get; set; } = new List<Trip>();
    }
}
