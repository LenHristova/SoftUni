namespace SoftUniCopy.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [MaxLength(200)]
        public string FullName { get; set; }

        public ICollection<StudentCourseInstance> EnrolledCourseInstances { get; set; } = new HashSet<StudentCourseInstance>();

        public ICollection<CourseInstance> LectureCourseInstances { get; set; } = new HashSet<CourseInstance>();
    }
}
