namespace P01_BillsPaymentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Enums;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 2)]
        [ValidPaymentMethodType]
        public PaymentMethodType Type { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Xor(relatedProperty: nameof(BankAccount), 
             xorTargetAttributes: new []{nameof(CreditCardId), nameof(CreditCard)})]
        public int? BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public virtual CreditCard CreditCard { get; set; }
    }
}
