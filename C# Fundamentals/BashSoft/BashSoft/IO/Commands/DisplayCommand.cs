using System;
using System.Collections.Generic;

using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("display")]
    public class DisplayCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDatabase _repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
        {
        }

        //Display database IF command consist 3 elements 
        //-> "display" command + entity to display ("students"/"courses") + order type ("ascending"/"descending")
        public override void Execute()
        {
            if (Data.Length != 3)
            {
                throw new InvalidCommandException(Input);
            }

            var entityToDisplay = Data[1];
            var sortType = Data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                var studentsComparator = CreateComparator<IStudent>(sortType);
                var list = _repository.GetAllStudentsSorted(studentsComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var courseComparator = CreateComparator<ICourse>(sortType);
                var list = _repository.GetAllCoursesSorted(courseComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(Input);
            }
        }

        private IComparer<T> CreateComparator<T>(string sortType)
            where T : IComparable<T>
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<T>.Create((first, second) => first.CompareTo(second));
            }

            if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<T>.Create((first, second) => second.CompareTo(first));
            }

            throw new InvalidCommandException(Input);
        }
    }
}