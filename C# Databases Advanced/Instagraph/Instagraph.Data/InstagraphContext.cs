using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    using Models;

    public class InstagraphContext : DbContext
    {
        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
            :base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(x => x.Username)
                    .IsUnique();

                entity
                    .HasOne(x => x.ProfilePicture)
                    .WithMany(y => y.Users)
                    .HasForeignKey(x => x.ProfilePictureId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity
                    .HasOne(x => x.User)
                    .WithMany(y => y.Comments)
                    .HasForeignKey(x => x.UserId);

                entity
                    .HasOne(x => x.Post)
                    .WithMany(y => y.Comments)
                    .HasForeignKey(x => x.PostId);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity
                    .HasOne(x => x.User)
                    .WithMany(y => y.Posts)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(x => x.Picture)
                    .WithMany(y => y.Posts)
                    .HasForeignKey(x => x.PictureId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.HasKey(x => new {x.UserId, x.FollowerId});

                entity
                    .HasOne(x => x.User)
                    .WithMany(y => y.Followers)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(x => x.Follower)
                    .WithMany(y => y.UsersFollowing)
                    .HasForeignKey(x => x.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
