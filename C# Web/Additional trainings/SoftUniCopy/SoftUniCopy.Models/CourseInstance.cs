namespace SoftUniCopy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseInstance
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public string LecturerId { get; set; }
        public User Lecturer { get; set; }

        public ICollection<StudentCourseInstance> Students { get; set; } = new HashSet<StudentCourseInstance>();

        public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
    }
}
