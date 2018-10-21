namespace SoftUniCopy.Models
{
    public class StudentCourseInstance
    {
        public string StudentId { get; set; }
        public User Student { get; set; }

        public int CourseInstanceId { get; set; }
        public CourseInstance CourseInstance { get; set; }
    }
}
