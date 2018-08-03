namespace BusTickets.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BankAccount : EntityBase
    {
        public string AccountNumber { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
