namespace BusTickets.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Models;

    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder
                .HasOne(s => s.Town)
                .WithMany(t => t.Stations)
                .HasForeignKey(s => s.TownId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
