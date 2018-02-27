using System;

namespace P03_Mankind.Models
{
    public class Student : Human
    {
        private string _facultyNumber;

        private string FacultyNumber
        {
            get => _facultyNumber;
            set
            {
                Validator.ValidateFacultyNumber(value);
                _facultyNumber = value;
            }
        }

        public Student(string firstName, string lastName, string facultyNumber) : base(firstName, lastName)
        {
            FacultyNumber = facultyNumber;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}" +
                   $"Faculty number: {FacultyNumber}";
        }
    }
}