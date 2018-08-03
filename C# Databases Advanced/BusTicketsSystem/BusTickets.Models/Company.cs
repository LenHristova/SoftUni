namespace BusTickets.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company : EntityBase
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public int NationalityId { get; set; }
        public virtual Country Nationality { get; set; }


        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
