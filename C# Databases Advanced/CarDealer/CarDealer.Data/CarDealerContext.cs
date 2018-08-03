using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    using Models;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext(DbContextOptions options) : base(options)
        {
        }

        public CarDealerContext()
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<CarPart> CarsParts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarPart>(entity =>
            {
                entity.HasKey(pc => new {pc.PartId, pc.CarId});

                entity.HasOne(pc => pc.Part)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(pc => pc.PartId);

                entity.HasOne(pc => pc.Car)
                    .WithMany(c => c.Parts)
                    .HasForeignKey(pc => pc.CarId);
            });
        }
    }
}
