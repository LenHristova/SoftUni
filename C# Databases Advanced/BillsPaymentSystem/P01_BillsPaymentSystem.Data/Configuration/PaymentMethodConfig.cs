namespace P01_BillsPaymentSystem.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder
                .HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods);

            builder
                .HasOne(pm => pm.BankAccount)
                .WithOne(ba => ba.PaymentMethod);

            builder
                .HasOne(pm => pm.CreditCard)
                .WithOne(cr => cr.PaymentMethod);
        }
    }
}
