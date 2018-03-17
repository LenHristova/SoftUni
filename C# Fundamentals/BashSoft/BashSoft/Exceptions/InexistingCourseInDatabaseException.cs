using System;

namespace BashSoft.Exceptions
{
    public class InexistingCourseInDatabaseException : Exception
    {
        private const string INEXISTING_COURSE = "The course you are trying to get does not exist in the data base!";

        public InexistingCourseInDatabaseException() : base(INEXISTING_COURSE) { }

        public InexistingCourseInDatabaseException(string message) : base(message) { }
    }
}
