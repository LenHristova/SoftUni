namespace SoftUniCopy.Models
{
    using System.Collections.Generic;

    public class Course
    {
        public int Id { get; set; }

        public ICollection<CourseInstance> Instances { get; set; } = new HashSet<CourseInstance>();
    }
}
