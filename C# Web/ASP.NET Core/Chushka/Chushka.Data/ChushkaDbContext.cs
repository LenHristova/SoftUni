namespace Chushka.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ChushkaDbContext : IdentityDbContext<User>
    {
        public ChushkaDbContext(DbContextOptions<ChushkaDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity
                    .HasOne(e => e.Product)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(e => e.ProductId);

                entity
                    .HasOne(e => e.Client)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(e => e.ClientId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}