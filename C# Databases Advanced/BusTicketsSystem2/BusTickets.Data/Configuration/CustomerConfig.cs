namespace BusTickets.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder
                .Property(c => c.HomeTownId)
                .IsRequired(false);

            builder
                .HasOne(c => c.BankAccount)
                .WithOne(ba => ba.Customer)
                .HasForeignKey<BankAccount>(ba => ba.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.HomeTown)
                .WithMany(t => t.Customers)
                .HasForeignKey(c => c.HomeTownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
