namespace BashSoft
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;

        //For every course stores course's name and students (usernames) in that course
        //For every student stores list with student's grades
        private static Dictionary<string, Dictionary<string, List<int>>> _studentsByCourse;

        //Initializes data if not yet
        public static void InitializeData(string fileName)
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                _studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        //Reads data from file and save it to the repo 
        private static void ReadData(string fileName)
        {
            var path = $"{SessionData.currentPath}\\{fileName}";
            if (File.Exists(path))
            {
                //pattern for data's valid format
                var pattern = @"([A-Z][a-zA-Z#+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)";
                var rgx = new Regex(pattern);

                var allInputLines = File.ReadAllLines(path);

                //Updates data line by line
                for (int line = 0; line < allInputLines.Length; line++)
                {
                    //Validates data -> not null or empty string and in the correct format
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        var currentMatch = rgx.Match(allInputLines[line]);
                        var courseName = currentMatch.Groups[1].Value;
                        var username = currentMatch.Groups[2].Value;
                        var hasParsedScore = int.TryParse(currentMatch.Groups[3].Value, out int studentScoreOnTask);

                        //if score is number from 0 to 100 -> updates data for course and student
                        if (hasParsedScore && studentScoreOnTask >= 0 && studentScoreOnTask <= 100)
                        {
                            //If current course doesn't exist in the repo -> add it
                            if (!_studentsByCourse.ContainsKey(courseName))
                            {
                                _studentsByCourse.Add(courseName, new Dictionary<string, List<int>>());
                            }

                            //If current student doesn't exist in current course -> add it
                            if (!_studentsByCourse[courseName].ContainsKey(username))
                            {
                                _studentsByCourse[courseName].Add(username, new List<int>());
                            }

                            //Adds current student's grades to the repo
                            _studentsByCourse[courseName][username].Add(studentScoreOnTask);
                        }
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            //Query for course is possible if data is initialized
            if (isDataInitialized)
            {
                //And query for course is possible if course exist in the repo
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

        private static bool IsQueryForStudentPossiblе(string courseName, string studentUsername)
        {
            //Query for student is possible if data is initialized 
            //and current course exist in the repo 
            if (IsQueryForCoursePossible(courseName))
            {
                // Query for student is possible if student exist in the repo
                if (_studentsByCourse[courseName].ContainsKey(studentUsername))
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

        //Gets given student's scores from given course if quary is possible
        public static void GetStudentScoresFromCourse(string courseName, string studentUsername)
        {
            if (IsQueryForStudentPossiblе(courseName, studentUsername))
            {
                var student =
                    new KeyValuePair<string, List<int>>(studentUsername,
                        _studentsByCourse[courseName][studentUsername]);
                OutputWriter.PrintStudent(student);
            }
        }

        //Gets all students from given course if quary is possible
        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in _studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(student);
                }
            }
        }

        public static void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = _studentsByCourse[courseName].Count;
                }
                else if (studentsToTake > _studentsByCourse[courseName].Count)
                {
                    studentsToTake = _studentsByCourse[courseName].Count;
                }

                RepositoryFilters.FilterAndTake(_studentsByCourse[courseName], givenFilter, studentsToTake.Value);
            }
        }

        public static void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = _studentsByCourse[courseName].Count;
                }
                else if (studentsToTake > _studentsByCourse[courseName].Count)
                {
                    studentsToTake = _studentsByCourse[courseName].Count;
                }

                RepositorySorters.OrderAndTake(_studentsByCourse[courseName], comparison, studentsToTake.Value);
            }
        }
    }
}
