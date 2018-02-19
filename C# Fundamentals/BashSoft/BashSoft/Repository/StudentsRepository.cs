using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BashSoft.IO;
using BashSoft.Models;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class StudentsRepository
    {
        //For every course stores course's name and students (usernames) in that course
        //For every student stores list with student's grades
        //private Dictionary<string, Dictionary<string, List<int>>> _studentsByCourse;
        private Dictionary<string, Course> _courses;
        private Dictionary<string, Student> _students;
        private bool _isDataInitialized;
        private readonly RepositoryFilter _filter;
        private readonly RepositorySorter _sorter;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            _filter = filter;
            _sorter = sorter;
        }

        //Loading data if not yet
        public void LoadData(string fileName)
        {
            if (_isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
                return;
            }

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            _students = new Dictionary<string, Student>();
            _courses = new Dictionary<string, Course>();
            ReadData(fileName);
        }

        //Unloading data if is initialized yet
        public void UnloadData()
        {
            if (!_isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
                return;
            }

            _students = null;
            _courses = null;
            _isDataInitialized = false;
        }

        //Reads data from file and save it to the repo 
        private void ReadData(string fileName)
        {
            var path = $"{SessionData.currentPath}\\{fileName}";
            if (File.Exists(path))
            {
                //pattern for data's valid format
                var pattern = @"([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                var rgx = new Regex(pattern);

                var allInputLines = File.ReadAllLines(path);

                //Updates data line by line
                for (var line = 0; line < allInputLines.Length; line++)
                {
                    //Validates data -> not null or empty string and in the correct format
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        var currentMatch = rgx.Match(allInputLines[line]);
                        var courseName = currentMatch.Groups[1].Value;
                        var username = currentMatch.Groups[2].Value;
                        var scoresStr = currentMatch.Groups[3].Value;

                        try
                        {
                            var scores = scoresStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

                            //Every score must be in range 0-100
                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                            }

                            //Number of scores must correspondent with number of tasks on exam
                            //scores's number can not be greater then task's number
                            if (scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            //If current student doesn't exist in current course -> add it
                            if (!_students.ContainsKey(username))
                            {
                                _students.Add(username, new Student(username));
                            }

                            //If current course doesn't exist in the repo -> add it
                            if (!_courses.ContainsKey(courseName))
                            {
                                _courses.Add(courseName, new Course(courseName));
                            }

                            var student = _students[username];
                            var course = _courses[courseName];

                            //Add info for current course and mark to the student
                            student.EnrollInCourse(course);
                            student.SetMarksInCourse(courseName, scores);

                            //Add info for student in given course
                            course.EnrollStudent(student);
                        }
                        catch (Exception ex)
                        {
                            OutputWriter.DisplayException($"{ex.Message} at line: {line}");
                        }
                    }
                }

                _isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            //Query for course is possible if data is initialized
            if (_isDataInitialized)
            {
                //And query for course is possible if course exist in the repo
                if (_courses.ContainsKey(courseName))
                {
                    return true;
                }

                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentUsername)
        {
            //Query for student is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            //And query for student is possible if student exist in the repo
            if (IsQueryForCoursePossible(courseName) &&
                _courses[courseName]._studentsByName.ContainsKey(studentUsername))
            {
                return true;
            }

            OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
            return false;
        }

        //Gets given student's scores from given course if quary is possible
        public void GetStudentScoresFromCourse(string courseName, string studentUsername)
        {
            if (IsQueryForStudentPossiblе(courseName, studentUsername))
            {
                var student =
                    new KeyValuePair<string, double>(studentUsername,
                        _courses[courseName]._studentsByName[studentUsername]._marksByCourses[courseName]);
                OutputWriter.PrintStudent(student);
            }
        }

        //Gets all students from given course if quary is possible
        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in _courses[courseName]._studentsByName)
                {
                    GetStudentScoresFromCourse(courseName, student.Key);
                }
            }
        }

        //Filters and takes students by given filter (excellent/average/poor)
        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            //Query is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            if (IsQueryForCoursePossible(courseName))
            {
                //If count of students to take is not given -> take all
                if (studentsToTake == null)
                {
                    studentsToTake = _courses[courseName]._studentsByName.Count;
                }

                var marks = _courses[courseName]._studentsByName
                    .ToDictionary(x => x.Key, x => x.Value._marksByCourses[courseName]);

                _filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        //Orders and takes students by given criterion (ascending/descending)       
        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            //Query is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            if (IsQueryForCoursePossible(courseName))
            {
                //If count of students to take is not given -> take all
                if (studentsToTake == null)
                {
                    studentsToTake = _courses[courseName]._studentsByName.Count;
                }

                var marks = _courses[courseName]._studentsByName
                    .ToDictionary(x => x.Key, x => x.Value._marksByCourses[courseName]);
                _sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}
