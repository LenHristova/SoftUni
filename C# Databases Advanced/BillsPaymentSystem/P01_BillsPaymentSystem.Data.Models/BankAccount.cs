namespace P01_BillsPaymentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        public decimal Balance { get; private set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Bank name must have at least 2 characters.")]
        public string BankName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "SWIFT must have at least 8 characters.")]
        [NonUnicode(ErrorMessage = "SWIFT must be non-unicode")]
        public string SwiftCode { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                this.Balance += amount;
                return true;
            }

            return false;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && this.Balance - amount >= 0)
            {
                this.Balance -= amount;
                return true;
            }

                return false;
        }
    }
}
