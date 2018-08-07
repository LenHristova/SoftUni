using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    using Models;
    using Models.Enums;

    public class FastFoodDbContext : DbContext
	{
		public FastFoodDbContext()
		{
		}

		public FastFoodDbContext(DbContextOptions options)
			: base(options)
		{
		}

	    public DbSet<Category> Categories { get; set; }
	    public DbSet<Employee> Employees { get; set; }
	    public DbSet<Item> Items { get; set; }
	    public DbSet<Order> Orders { get; set; }
	    public DbSet<OrderItem> OrderItems { get; set; }
	    public DbSet<Position> Positions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!builder.IsConfigured)
			{
				builder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
		    builder.Entity<Employee>(entity =>
		    {
		        entity
		            .HasOne(x => x.Position)
		            .WithMany(y => y.Employees)
		            .HasForeignKey(x => x.PositionId);
		    });


		    builder.Entity<Item>(entity =>
		    {
		        entity
		            .HasOne(x => x.Category)
		            .WithMany(y => y.Items)
		            .HasForeignKey(x => x.CategoryId);
		    });


            builder.Entity<Position>(entity =>
		    {
		        entity.HasIndex(x => x.Name)
		            .IsUnique();
		    });


		    builder.Entity<Item>(entity =>
		    {
		        entity.HasIndex(x => x.Name)
		            .IsUnique();
		    });


		    builder.Entity<Order>(entity =>
		    {
		        entity.Property(x => x.Type)
		            .HasDefaultValue(OrderType.ForHere);

		        entity
		            .HasOne(x => x.Employee)
		            .WithMany(y => y.Orders)
		            .HasForeignKey(x => x.EmployeeId);
            });


		    builder.Entity<OrderItem>(entity =>
		    {
		        entity.HasKey(x => new {x.OrderId, x.ItemId});

		        entity
		            .HasOne(x => x.Order)
		            .WithMany(y => y.OrderItems)
		            .HasForeignKey(x => x.OrderId);

		        entity
		            .HasOne(x => x.Item)
		            .WithMany(y => y.OrderItems)
		            .HasForeignKey(x => x.ItemId);
            });
        }
	}
}