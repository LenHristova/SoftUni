namespace SoftUniCopy.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SoftUniCopyContext : IdentityDbContext<User>
    {
        public SoftUniCopyContext(DbContextOptions<SoftUniCopyContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseInstance> CourseInstances { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<StudentCourseInstance> StudentsCourseInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseInstance>(entity =>
            {
                entity
                    .HasOne(ci => ci.Course)
                    .WithMany(c => c.Instances)
                    .HasForeignKey(ci => ci.CourseId);

                entity
                    .HasOne(ci => ci.Lecturer)
                    .WithMany(l => l.LectureCourseInstances)
                    .HasForeignKey(ci => ci.LecturerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Lecture>(entity =>
            {
                entity
                    .HasOne(l => l.CourseInstance)
                    .WithMany(c => c.Lectures)
                    .HasForeignKey(l => l.CourseInstanceId);
            });

            builder.Entity<Resource>(entity =>
            {
                entity
                    .HasOne(r => r.Lecture)
                    .WithMany(l => l.Resources)
                    .HasForeignKey(r => r.LectureId);
            });

            builder.Entity<StudentCourseInstance>(entity =>
            {
                entity
                    .HasKey(k => new { k.StudentId, k.CourseInstanceId });

                entity
                    .HasOne(sci => sci.CourseInstance)
                    .WithMany(c => c.Students)
                    .HasForeignKey(sci => sci.CourseInstanceId);

                entity
                    .HasOne(sci => sci.Student)
                    .WithMany(s => s.EnrolledCourseInstances)
                    .HasForeignKey(sci => sci.StudentId);
            });

            base.OnModelCreating(builder);
        }
    }
}
