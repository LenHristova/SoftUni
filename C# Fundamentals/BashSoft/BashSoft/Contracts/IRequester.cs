using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetAllStudentsFromCourse(string courseName);

        void GetStudentScoresFromCourse(string courseName, string studentUsername);

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> comparer);

        ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> comparer);
    }
}
