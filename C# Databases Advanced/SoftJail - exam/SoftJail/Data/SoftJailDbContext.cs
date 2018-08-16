namespace SoftJail.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SoftJailDbContext : DbContext
    {
        public SoftJailDbContext()
        {
        }

        public SoftJailDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Cell> Cells { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }
        public DbSet<Prisoner> Prisoners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cell>(entity =>
            {

                entity
                    .HasOne(x => x.Department)
                    .WithMany(y => y.Cells)
                    .HasForeignKey(x => x.DepartmentId);

            });

            builder.Entity<Mail>(entity =>
            {

                entity
                    .HasOne(x => x.Prisoner)
                    .WithMany(y => y.Mails)
                    .HasForeignKey(x => x.PrisonerId);

            });

            builder.Entity<Officer>(entity =>
            {

                entity
                    .HasOne(x => x.Department)
                    .WithMany(y => y.Officers)
                    .HasForeignKey(x => x.DepartmentId);

            });

            builder.Entity<Prisoner>(entity =>
            {

                entity
                    .HasOne(x => x.Cell)
                    .WithMany(y => y.Prisoners)
                    .HasForeignKey(x => x.CellId);

            });

            builder.Entity<OfficerPrisoner>(entity =>
            {
                entity
                    .HasKey(x => new { x.OfficerId, x.PrisonerId });

                entity
                    .HasOne(x => x.Prisoner)
                    .WithMany(y => y.PrisonerOfficers)
                    .HasForeignKey(x => x.PrisonerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(x => x.Officer)
                    .WithMany(y => y.OfficerPrisoners)
                    .HasForeignKey(x => x.OfficerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}