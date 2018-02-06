namespace BashSoft
{
    using System.Collections.Generic;
    using System.IO;

    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> _studentsByCourse;

        public static void InitializeData()
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                _studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData();
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        private static void ReadData()
        {
            using (var reader = new StreamReader("./Resources/data.txt"))
            {
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    if (input == string.Empty)
                    {
                        continue;
                    }

                    var tokens = input.Split(' ');
                    var course = tokens[0];
                    var student = tokens[1];
                    var mark = int.Parse(tokens[2]);

                    if (!_studentsByCourse.ContainsKey(course))
                    {
                        _studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                    }

                    if (!_studentsByCourse[course].ContainsKey(student))
                    {
                        _studentsByCourse[course].Add(student, new List<int>());
                    }

                    _studentsByCourse[course][student].Add(mark);
                }
            }

            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (_studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private static bool IsQueryForStudentPossiblе(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (_studentsByCourse[courseName].ContainsKey(studentUserName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
                }
            }

            return false;
        }

        public static void GetStudentScoresFromCourse(string courseName, string studentUserName)
        {
            if (IsQueryForStudentPossiblе(courseName, studentUserName))
            {
                var student =
                    new KeyValuePair<string, List<int>>(studentUserName,
                        _studentsByCourse[courseName][studentUserName]);
                OutputWriter.DisplayStudent(student);
            }
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in _studentsByCourse[courseName])
                {
                    OutputWriter.DisplayStudent(student);
                }
            }
        }
    }
}
