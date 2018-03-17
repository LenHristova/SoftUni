using System.Collections.Generic;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Course
    {
        public const int NUMBER_OF_TASKS_ON_EXAM = 5;
        public const int MAX_SCORE_ON_EXAM_TASK = 100;

        private string _name;
        private readonly Dictionary<string, Student> _studentsByName;

        public Course(string name)
        {
            Name = name;
            _studentsByName = new Dictionary<string, Student>();
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                _name = value;
            }
        }

        public IReadOnlyDictionary<string, Student> StudentsByName => _studentsByName;

        public void EnrollStudent(Student student)
        {
            if (_studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, Name);
            }

            _studentsByName.Add(student.Username, student);
        }
    }
}
