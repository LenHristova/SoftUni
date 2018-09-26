namespace FluffyDuffyMunchkinCats.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CatsContext : DbContext
    {
        public CatsContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Cat> Cats { get; set; }
    }
}
