using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        { }

        public StudentSystemContext(DbContextOptions options) 
            : base(options)
        { }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

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

            modelBuilder.Entity<Student>(entity =>
            {
                modelBuilder.Entity<Student>()
                    .Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                modelBuilder.Entity<Student>()
                    .Property(s => s.PhoneNumber)
                    .IsUnicode(false)
                    .HasColumnType("CHAR(10)");

                modelBuilder.Entity<Student>()
                    .HasData(
                        new Student{StudentId = 1, Name = "Pesho", PhoneNumber = "0123456789", RegisteredOn = DateTime.Now, Birthday = new DateTime(2000, 5, 21)},
                        new Student{StudentId = 2, Name = "Peshka", PhoneNumber = "0123456789", RegisteredOn = DateTime.Now, Birthday = new DateTime(2000, 5, 22)},
                        new Student { StudentId = 3, Name = "Gosho", PhoneNumber = "0123456789", RegisteredOn = DateTime.Now, Birthday = new DateTime(1985, 5, 22) }
                    );
            });

            modelBuilder.Entity<Course>(entity =>
            {
                modelBuilder.Entity<Course>()
                    .Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                modelBuilder.Entity<Course>()
                    .HasData(
                        new Course { CourseId = 1, Name = "C#", Description = "C# Advanced", StartDate = new DateTime(2018, 8, 11), EndDate = new DateTime(2018, 9, 11), Price = 330},
                        new Course { CourseId = 2, Name = "Java", Description = "Java Advanced", StartDate = new DateTime(2018, 8, 11), EndDate = new DateTime(2018, 9, 11), Price = 330}
                    );
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                modelBuilder.Entity<Resource>()
                    .Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                modelBuilder.Entity<Resource>().
                    Property(r => r.Url)
                    .IsUnicode(false);

                modelBuilder.Entity<Resource>()
                    .HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);

                modelBuilder.Entity<Resource>()
                    .HasData(
                        new Resource{ResourceId = 1, Name = "C#", Url = "url", CourseId = 1, ResourceType = ResourceType.Document},
                        new Resource{ResourceId = 2, Name = "C# Video", Url = "url", CourseId = 1, ResourceType = ResourceType.Video}
                        );
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                modelBuilder.Entity<Homework>()
                    .Property(h => h.Content)
                    .IsUnicode(false);

                modelBuilder.Entity<Homework>()
                    .HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.StudentId);

                modelBuilder.Entity<Homework>()
                    .HasOne(h => h.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(h => h.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                modelBuilder.Entity<StudentCourse>()
                    .HasKey(sc => new { sc.StudentId, sc.CourseId });

                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(sc => sc.StudentId);

                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(sc => sc.CourseId);

                modelBuilder.Entity<StudentCourse>()
                    .HasData(
                        new StudentCourse {CourseId = 1, StudentId = 1},
                        new StudentCourse {CourseId = 1, StudentId = 2},
                        new StudentCourse {CourseId = 2, StudentId = 1}
                    );
            });
        }
        }
}
