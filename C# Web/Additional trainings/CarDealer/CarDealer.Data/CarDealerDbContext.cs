namespace CarDealer.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDealerDbContext : IdentityDbContext
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<PartCar> PartCars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Sale>( entity =>
            {
               entity
                    .HasOne(s => s.Car)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CarId);

                entity
                    .HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);
            });

            builder.Entity<PartCar>(entity =>
            {
                entity
                    .HasKey(pc => new {pc.PartId, pc.CarId});

                entity
                    .HasOne(pc => pc.Car)
                    .WithMany(c => c.Parts)
                    .HasForeignKey(pc => pc.CarId);

                entity
                    .HasOne(pc => pc.Part)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(pc => pc.PartId);
            });

            builder.Entity<Part>(entity =>
            {
                entity
                    .HasOne(p => p.Supplier)
                    .WithMany(s => s.Parts)
                    .HasForeignKey(p => p.SupplierId);
            });
        }
    }
}
