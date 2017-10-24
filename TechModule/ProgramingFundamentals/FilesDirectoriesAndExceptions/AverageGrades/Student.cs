using System.Collections.Generic;
using System.Linq;

namespace AverageGrades
{
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
}
