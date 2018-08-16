namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SeatingClass
    {
        //•	Id – integer, Primary Key
        //•	Name – text with max length 30 (required, unique)
        //•	Abbreviation – text with an exact length of 2 (no more, no less), (required, unique)

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        //unique
        public string Name { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [Column(TypeName = "CHAR(2)")]
        public string Abbreviation { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; } = new List<TrainSeat>();
    }
}
