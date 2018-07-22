using System;
using System.Linq;
using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                var students = db.Students
                    .Select(s => new
                    {
                        s.Name,
                        Courses = s.CourseEnrollments
                            .Select(se => se.Course.Name)
                    });

                foreach (var student in students)
                {
                    Console.WriteLine(student.Name);
                    Console.WriteLine(string.Join(Environment.NewLine, student.Courses));
                    Console.WriteLine("----------------------------------------------");
                }
            }
        }
    }
}
