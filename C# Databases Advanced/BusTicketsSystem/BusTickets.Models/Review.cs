namespace BusTickets.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

    public class Review : EntityBase
    {
        [Required]
        public string Content { get; set; }

        [Range(1, 10)]
        public double Grade { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public DateTime? PublishedDateTime { get; set; }
    }
}
