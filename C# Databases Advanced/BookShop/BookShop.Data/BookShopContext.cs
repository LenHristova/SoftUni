using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext()
        { }

        public BookShopContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books);
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(bc => new {bc.BookId, bc.CategoryId});

                entity.HasOne(bc => bc.Book)
                    .WithMany(b => b.BookCategories);

                entity.HasOne(bc => bc.Category)
                    .WithMany(b => b.CategoryBooks);
            });
        }
    }
}
