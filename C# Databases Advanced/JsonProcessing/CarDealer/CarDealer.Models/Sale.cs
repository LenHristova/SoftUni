namespace CarDealer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        [ForeignKey(nameof(Sale.Car))]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [ForeignKey(nameof(Sale.Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
