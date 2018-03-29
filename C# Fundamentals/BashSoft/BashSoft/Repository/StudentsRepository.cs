using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using BashSoft.Contracts;
using BashSoft.DataStructures;
using BashSoft.Exceptions;
using BashSoft.IO;
using BashSoft.Models;

namespace BashSoft.Repository
{
    public class StudentsRepository : IDatabase, IRequester, IFilteredTaker, IOrderedTaker
    {
        private Dictionary<string, ICourse> _courses;
        private Dictionary<string, IStudent> _students;
        private bool _isDataInitialized;
        private readonly IDataFilter _filter;
        private readonly IDataSorter _sorter;

        public StudentsRepository(IDataFilter filter, IDataSorter sorter)
        {
            this._filter = filter;
            this._sorter = sorter;
        }

        //Loading data if not yet
        public void LoadData(string fileName)
        {
            if (this._isDataInitialized)
            {
                throw new DataAlreadyInitializedException();
            }

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            this._students = new Dictionary<string, IStudent>();
            this._courses = new Dictionary<string, ICourse>();
            this.ReadData(fileName);
        }

        //Unloading data if is initialized yet
        public void UnloadData()
        {
            if (!this._isDataInitialized)
            {
                throw new DataNotInitializedException();
            }

            this._students = null;
            this._courses = null;
            this._isDataInitialized = false;
        }

        //Reads data from file and save it to the repo 
        private void ReadData(string fileName)
        {
            var path = $"{SessionData.CurrentPath}\\{fileName}";
            if (!File.Exists(path))
            {
                throw new InvalidPathException();
            }

            //pattern for data's valid format
            const string pattern = @"^([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)$";
            var rgx = new Regex(pattern);

            var allInputLines = File.ReadAllLines(path);

            //Updates data line by line
            for (var line = 0; line < allInputLines.Length; line++)
            {
                //Valid line data is not null or empty string and in the correct format

                //If line data is invalid -> go to the next line
                if (string.IsNullOrEmpty(allInputLines[line]) ||
                    !rgx.IsMatch(allInputLines[line]))
                {
                    continue;
                }

                var currentMatch = rgx.Match(allInputLines[line]);
                var courseName = currentMatch.Groups[1].Value;
                var username = currentMatch.Groups[2].Value;
                var scoresStr = currentMatch.Groups[3].Value;

                try
                {
                    var scores = scoresStr.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    //Every score must be in range 0-100
                    if (scores.Any(x => x > 100 || x < 0))
                    {
                        throw new InvalidScoreException();
                    }

                    //Number of scores must correspondent with the number of tasks on exam
                    var isScoresCountGreaterThenTasksCount = scores.Length > Course.NUMBER_OF_TASKS_ON_EXAM;
                    if (isScoresCountGreaterThenTasksCount)
                    {
                        throw new InvalidNumberOfScoresException();
                    }

                    //Add student if doesn't exist in students' repo
                    if (!this._students.ContainsKey(username))
                    {
                        this._students.Add(username, new Student(username));
                    }

                    //Add course if doesn't exist in courses' repo
                    if (!this._courses.ContainsKey(courseName))
                    {
                        this._courses.Add(courseName, new Course(courseName));
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
                    OutputWriter.DisplayException($"{ex.Message.Trim('.')} at line: {line}");
                }
            }

            this._isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            //Query for course is possible if data is initialized
            if (!this._isDataInitialized)
            {
                throw new DataNotInitializedException();
            }

            //And query for course is possible if course exist in the repo
            if (!this._courses.ContainsKey(courseName))
            {
                throw new InexistingCourseInDatabaseException();
            }

            //If all checks is passed -> query is possible
            return true;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentUsername)
        {
            //Query for student is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            //And query for student is possible if student exist in the repo
            if (this.IsQueryForCoursePossible(courseName) &&
                this._courses[courseName].StudentsByName.ContainsKey(studentUsername))
            {
                return true;
            }

            throw new InexistingStudentInDatabaseException();
        }

        //Gets given student's scores from given course if query is possible
        public void GetStudentScoresFromCourse(string courseName, string studentUsername)
        {
            if (this.IsQueryForStudentPossiblе(courseName, studentUsername))
            {
                var studentMark = this._courses[courseName]
                    .StudentsByName[studentUsername]
                    .MarksByCourses[courseName];
                var student = new KeyValuePair<string, double>(studentUsername, studentMark);
                OutputWriter.PrintStudent(student);
            }
        }

        // Sort courses by given comparer
        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> comparer)
        {
            if (!this._isDataInitialized)
            {
                throw new DataNotInitializedException();
            }

            var sortedCourses = new SimpleSortedList<ICourse>(comparer);
            sortedCourses.AddAll(_courses.Values);

            return sortedCourses;
        }

        // Sort students by given comparer
        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> comparer)
        {
            if (!this._isDataInitialized)
            {
                throw new DataNotInitializedException();
            }

            var sortedStudents = new SimpleSortedList<IStudent>(comparer);
            sortedStudents.AddAll(_students.Values);

            return sortedStudents;

        }

        //Gets all students from given course if query is possible
        public void GetAllStudentsFromCourse(string courseName)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in this._courses[courseName].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, student.Key);
                }
            }
        }

        //Filters and takes students by given filter (excellent/average/poor)
        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            //Query is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            if (this.IsQueryForCoursePossible(courseName))
            {
                var studentsMarks = this.GetStudentsMarks(courseName, ref studentsToTake);
                this._filter.FilterAndTake(studentsMarks, givenFilter, studentsToTake.Value);
            }
        }

        //Orders and takes students by given criterion (ascending/descending)       
        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            //Query is possible if data is initialized and current course exist in the repo ->
            //IsQueryForCoursePossible method do this check
            if (this.IsQueryForCoursePossible(courseName))
            {
                var studentsMarks = this.GetStudentsMarks(courseName, ref studentsToTake);
                this._sorter.OrderAndTake(studentsMarks, comparison, studentsToTake.Value);
            }
        }

        private Dictionary<string, double> GetStudentsMarks(string courseName, ref int? studentsToTake)
        {
            //If count of students to take is not given -> take all
            if (studentsToTake == null)
            {
                studentsToTake = this._courses[courseName].StudentsByName.Count;
            }

            var studentsMarks = this._courses[courseName].StudentsByName
                .ToDictionary(x => x.Key, x => x.Value.MarksByCourses[courseName]);
            return studentsMarks;
        }
    }
}
