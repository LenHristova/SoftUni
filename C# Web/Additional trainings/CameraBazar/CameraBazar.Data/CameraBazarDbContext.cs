namespace CameraBazar.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CameraBazarDbContext : IdentityDbContext<User>
    {
        public CameraBazarDbContext(DbContextOptions<CameraBazarDbContext> options)
        : base(options) { }

        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user
                    .HasMany(u => u.Cameras)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId);
            });

            base.OnModelCreating(builder);
        }
    }
}
