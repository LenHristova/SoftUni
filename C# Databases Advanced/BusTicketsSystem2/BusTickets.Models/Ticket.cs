namespace BusTickets.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ticket : EntityBase
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(3)]
        public string Seat { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
