namespace Eventures.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventuresDbContext : IdentityDbContext<User>
    {
        public EventuresDbContext() {}

        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        public static string ConnectionString { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}
