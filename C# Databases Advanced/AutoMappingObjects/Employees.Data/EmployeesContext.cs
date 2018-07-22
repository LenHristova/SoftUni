namespace Employees.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
        { }

        public EmployeesContext(DbContextOptions options) 
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

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

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(e => e.Manager)
                    .WithMany(m => m.Employees)
                    .HasForeignKey(e => e.ManagerId);
            });
        }
    }
}
