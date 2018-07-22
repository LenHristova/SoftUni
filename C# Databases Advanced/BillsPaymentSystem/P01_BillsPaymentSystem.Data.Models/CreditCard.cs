namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        public CreditCard()
        {
            this.ChangeLimit(500);
        }

        [Key]
        public int CreditCardId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Limit { get; private set; }

        [Range(0, double.MaxValue)]
        public decimal MoneyOwed { get; private set; }

        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                this.MoneyOwed -= amount;
                return true;
            }

            return false;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && this.LimitLeft - amount >= 0)
            {
                this.MoneyOwed += amount;
                return true;
            }

            return false;
        }

        public bool ChangeLimit(decimal newLimit)
        {
            if (newLimit >= 0)
            {
                this.Limit = newLimit;
                return true;
            }

            return false;
        }
    }
}
