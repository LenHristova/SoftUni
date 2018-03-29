using System.Collections.Generic;
using System.Linq;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Student : IStudent
    {
        private string _username;
        private readonly Dictionary<string, ICourse> _enrolledCourses;
        private readonly Dictionary<string, double> _marksByCourses;

        public Student(string username)
        {
            Username = username;
            _enrolledCourses = new Dictionary<string, ICourse>();
            _marksByCourses = new Dictionary<string, double>();
        }

        public string Username
        {
            get => _username;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                _username = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses => _enrolledCourses;

        public IReadOnlyDictionary<string, double> MarksByCourses => _marksByCourses;

        public void EnrollInCourse(ICourse course)
        {
            if (_enrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(Username, course.Name);
            }

            _enrolledCourses.Add(course.Name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!_enrolledCourses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException();
            }

            if (scores.Length > Course.NUMBER_OF_TASKS_ON_EXAM)
            {
                throw new InvalidNumberOfScoresException();
            }

            _marksByCourses.Add(courseName, CalculateMark(scores));
        }

        private static double CalculateMark(IEnumerable<int> scores)
        {
            var persentageOfSolvedExam =
                scores.Sum() / (double)(Course.NUMBER_OF_TASKS_ON_EXAM * Course.MAX_SCORE_ON_EXAM_TASK);
            var mark = persentageOfSolvedExam * 4 + 2;
            return mark;
        }

        public int CompareTo(IStudent other) => Username.CompareTo(other.Username);

        public override string ToString() => Username;
    }
}
