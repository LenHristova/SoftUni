using System;
using System.Collections.Generic;

using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("display")]
    public class DisplayCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public DisplayCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Display database IF command consist 3 elements 
        //-> "display" command + entity to display ("students"/"courses") + order type ("ascending"/"descending")
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 3)
            {
                throw new InvalidCommandException(input);
            }

            var entityToDisplay = data[1];
            var sortType = data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                var studentsComparator = CreateComparator<IStudent>(sortType, input);
                var list = _repository.GetAllStudentsSorted(studentsComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var courseComparator = CreateComparator<ICourse>(sortType, input);
                var list = _repository.GetAllCoursesSorted(courseComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(input);
            }
        }

        private IComparer<T> CreateComparator<T>(string sortType, string input)
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

            throw new InvalidCommandException(input);
        }
    }
}