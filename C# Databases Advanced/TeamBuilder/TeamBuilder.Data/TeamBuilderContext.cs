namespace TeamBuilder.Data
{
    using Configuration;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        { }

        public TeamBuilderContext(DbContextOptions options) 
        : base(options)
        { }

        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamEvent> TeamEvents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TeamConfig());
            builder.ApplyConfiguration(new TeamEventConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new UserTeamConfig());
        }
    }
}
