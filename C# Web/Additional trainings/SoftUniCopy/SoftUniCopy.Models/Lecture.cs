namespace SoftUniCopy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Lecture
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        [MaxLength(200)]
        public string LectureHall { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int CourseInstanceId { get; set; }
        public CourseInstance CourseInstance { get; set; }

        public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();

        //TODO homework descriptor
    }
}