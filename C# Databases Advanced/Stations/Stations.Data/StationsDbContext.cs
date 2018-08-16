using Microsoft.EntityFrameworkCore;

namespace Stations.Data
{
    using Models;

    public class StationsDbContext : DbContext
	{
		public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}

	    public DbSet<CustomerCard> Cards { get; set; }
	    public DbSet<SeatingClass> SeatingClasses { get; set; }
	    public DbSet<Station> Stations { get; set; }
	    public DbSet<Ticket> Tickets { get; set; }
	    public DbSet<Train> Trains { get; set; }
	    public DbSet<TrainSeat> TrainSeats { get; set; }
	    public DbSet<Trip> Trips { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    modelBuilder.Entity<Station>(entity =>
		    {
		        entity.HasIndex(x => x.Name)
		            .IsUnique();
		    });

		    modelBuilder.Entity<Train>(entity =>
		    {
		        entity.HasIndex(x => x.TrainNumber)
		            .IsUnique();
		    });

		    modelBuilder.Entity<SeatingClass>(entity =>
		    {
		        entity.HasIndex(x => x.Name)
		            .IsUnique();

		        entity.HasIndex(x => x.Abbreviation)
		            .IsUnique();
            });

		    modelBuilder.Entity<Ticket>(entity =>
		    {
		        entity
		            .HasOne(x => x.Trip)
		            .WithMany(y => y.Tickets)
		            .HasForeignKey(x => x.TripId);

		        entity
		            .HasOne(x => x.CustomerCard)
		            .WithMany(y => y.BoughtTickets)
		            .HasForeignKey(x => x.CustomerCardId);
            });

		    modelBuilder.Entity<TrainSeat>(entity =>
		    {
		        entity
		            .HasOne(x => x.Train)
		            .WithMany(y => y.TrainSeats)
		            .HasForeignKey(x => x.TrainId);

                entity
		            .HasOne(x => x.SeatingClass)
		            .WithMany(y => y.TrainSeats)
		            .HasForeignKey(x => x.SeatingClassId);
		    });

		    modelBuilder.Entity<Trip>(entity =>
		    {
		        entity
		            .HasOne(x => x.OriginStation)
		            .WithMany(y => y.TripsFrom)
		            .HasForeignKey(x => x.OriginStationId)
		            .OnDelete(DeleteBehavior.Restrict);

		        entity
		            .HasOne(x => x.DestinationStation)
		            .WithMany(y => y.TripsTo)
		            .HasForeignKey(x => x.DestinationStationId);

                entity
		            .HasOne(x => x.Train)
		            .WithMany(y => y.Trips)
		            .HasForeignKey(x => x.TrainId);
		    });
        }
	}
}