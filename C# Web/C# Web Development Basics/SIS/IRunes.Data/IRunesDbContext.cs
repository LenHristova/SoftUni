namespace IRunes.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    // ReSharper disable once InconsistentNaming
    public class IRunesDbContext : DbContext
    {
        public IRunesDbContext() { }

        public IRunesDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<TrackAlbum> TracksAlbums { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrackAlbum>(entity =>
            {
                entity
                    .HasKey(ta => new { ta.TrackId, ta.AlbumId });

                entity
                    .HasOne(ta => ta.Album)
                    .WithMany(a => a.Tracks)
                    .HasForeignKey(ta => ta.AlbumId);

                entity
                    .HasOne(ta => ta.Track)
                    .WithMany(t => t.Albums)
                    .HasForeignKey(ta => ta.TrackId);
            });
        }
    }
}
