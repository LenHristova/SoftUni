namespace BusTickets.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Town : EntityBase
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Station> Stations { get; set; } = new List<Station>();

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
