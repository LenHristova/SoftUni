namespace BusTickets.Models
{
    using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

    public class Country : EntityBase
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; } = new List<Town>();

        public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
    }
}
