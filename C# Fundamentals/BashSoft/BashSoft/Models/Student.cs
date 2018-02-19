using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Models
{
    public class Student
    {
        public string _username;
        public Dictionary<string, Course> _enrolledCourses;
        public Dictionary<string, double> _marksByCourses;

        public Student(string username)
        {
            _username = username;
            _enrolledCourses = new Dictionary<string, Course>();
            _marksByCourses = new Dictionary<string, double>();
        }

        public void EnrollInCourse(Course course)
        {
            if (_enrolledCourses.ContainsKey(course._name))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, _username, course._name));
                return;
            }

            _enrolledCourses.Add(course._name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!_enrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                return;
            }

            _marksByCourses.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            var persentageOfSolvedExam =
                scores.Sum() / (double) (Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            var mark = persentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}
