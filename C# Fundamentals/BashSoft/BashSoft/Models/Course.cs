using System.Collections.Generic;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Models
{
    public class Course
    {
        public string _name;
        public Dictionary<string, Student> _studentsByName;
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public Course(string name)
        {
            _name = name;
            _studentsByName = new Dictionary<string, Student>();
        }

        public void EnrollStudent(Student student)
        {
            if (_studentsByName.ContainsKey(student._username))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, student._username, _name));
                return;
            }

            _studentsByName.Add(student._username, student);
        }
    }
}
