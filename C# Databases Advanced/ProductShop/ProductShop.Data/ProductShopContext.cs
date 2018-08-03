namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProductShopContext : DbContext
    {
        public ProductShopContext()
        {
        }

        public ProductShopContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity
                    .HasOne(p => p.Buyer)
                    .WithMany(b => b.BuyedProducts)
                    .HasForeignKey(p => p.BuyerId);

                entity
                    .HasOne(p => p.Seller)
                    .WithMany(s => s.SoldProducts)
                    .HasForeignKey(p => p.SellerId);
            });

            builder.Entity<CategoryProduct>(entity =>
            {
                entity.HasKey(cp => new {cp.CategoryId, cp.ProductId});

                entity
                    .HasOne(cp => cp.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(cp => cp.CategoryId);

                entity
                    .HasOne(cp => cp.Product)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(cp => cp.ProductId);
            });
        }
    }
}
