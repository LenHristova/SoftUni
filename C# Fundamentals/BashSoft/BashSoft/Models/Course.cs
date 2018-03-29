using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Course : ICourse
    {
        public const int NUMBER_OF_TASKS_ON_EXAM = 5;
        public const int MAX_SCORE_ON_EXAM_TASK = 100;

        private string _name;
        private readonly Dictionary<string, IStudent> _studentsByName;

        public Course(string name)
        {
            Name = name;
            _studentsByName = new Dictionary<string, IStudent>();
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

        public IReadOnlyDictionary<string, IStudent> StudentsByName => _studentsByName;

        public void EnrollStudent(IStudent student)
        {
            if (_studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, Name);
            }

            _studentsByName.Add(student.Username, student);
        }

        public int CompareTo(ICourse other) => Name.CompareTo(other.Name);

        public override string ToString() => Name;
    }
}
