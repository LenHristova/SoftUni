using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public List<double> Marks { get; set; }
    public double AvarageGrade => Marks.Average();

    public override string ToString()
    {
        return $"{Name} -> {AvarageGrade:F2}";
    }
}
class StartUp
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        int studentsCount = int.Parse(Console.ReadLine());
        for (int currStudent = 0; currStudent < studentsCount; currStudent++)
        {
            string[] studentInfo = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            students.Add(new Student
            {
                Name = studentInfo[0],
                Marks = studentInfo.Skip(1).Select(double.Parse).ToList()
            });
        }

        List<Student> topStudents = students
            .Where(s => s.AvarageGrade >= 5)
            .OrderBy(s => s.Name)
            .ThenByDescending(s => s.AvarageGrade)
            .ToList();

        Console.WriteLine(string.Join(Environment.NewLine, topStudents));
    }
}