using System;
using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class DisplayCommand : Command, IExecutable
    {
        public DisplayCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

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
                var list = Repository.GetAllStudentsSorted(studentsComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }

            if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var courseComparator = CreateComparator<ICourse>(sortType);
                var list = Repository.GetAllCoursesSorted(courseComparator);
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

        //public override void Execute()
        //{
        //    if (Data.Length != 3)
        //    {
        //        throw new InvalidCommandException(Input);
        //    }

        //    var entityToDisplay = Data[1];
        //    var sortType = Data[2];

        //    if (entityToDisplay.Equals("strudents", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var studentsComparator = CreateStudentComparator(sortType);
        //        var list = Repository.GetAllStudentsSorted(studentsComparator);
        //        OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
        //    }

        //    if (entityToDisplay.Equals("strudents", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var courseComparator = CreateCourseComparator(sortType);
        //        var list = Repository.GetAllCoursesSorted(courseComparator);
        //        OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
        //    }
        //    else
        //    {
        //        throw new InvalidCommandException(Input);
        //    }
        //}

        //private IComparer<ICourse> CreateCourseComparator(string sortType)
        //{
        //    throw new NotImplementedException();
        //}

        //private IComparer<IStudent> CreateStudentComparator(string sortType)
        //{
        //    if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return Comparer<IStudent>.Create((studentOne, studentTwo) => studentOne.CompareTo(studentTwo))
        //    }

        //    if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return Comparer<IStudent>.Create((studentOne, studentTwo) => studentTwo.CompareTo(studentOne));
        //    }

        //    throw new InvalidCommandException(Input);
        //}
    }
}