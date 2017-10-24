using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AverageGrades
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            string[] lines = File.ReadAllLines(@"..\..\input.txt");

            var students = new List<Student>();

            for (int pos = 1; pos < lines.Length; pos++)
            {
                if (int.TryParse(lines[pos], out int _))
                {
                    AppandTopStudentsInfoToFile(students);

                    students.Clear();
                    continue;
                }

                var studentInfo = lines[pos]
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var name = studentInfo[0];
                var marks = studentInfo
                    .Skip(1)
                    .Select(double.Parse)
                    .ToList();

                students.Add(new Student
                {
                    Name = name,
                    Marks = marks
                });
            }

            AppandTopStudentsInfoToFile(students);
        }

        private static void AppandTopStudentsInfoToFile(List<Student> students)
        {
            students = students
                .Where(st => st.AvarageGrade >= 5)
                .OrderBy(st => st.Name)
                .ThenByDescending(st => st.AvarageGrade)
                .ToList();

            foreach (var student in students)
            {
                File.AppendAllText(@"..\..\output.txt", student + Environment.NewLine);
            }

            File.AppendAllText(@"..\..\output.txt", "----------------------" + Environment.NewLine);
        }
    }
}
