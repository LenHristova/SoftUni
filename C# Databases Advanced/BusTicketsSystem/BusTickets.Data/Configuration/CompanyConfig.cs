namespace BusTickets.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasOne(c => c.Nationality)
                .WithMany(n => n.Companies)
                .HasForeignKey(c => c.NationalityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
