using System;

namespace BashSoft.Exceptions
{
    public class InexistingStudentInDatabaseException : Exception
    {
        private const string INEXISTING_STUDENT = "The user name for the student you are trying to get does not exist!";

        public InexistingStudentInDatabaseException() : base(INEXISTING_STUDENT) { }

        public InexistingStudentInDatabaseException(string message) : base(message) { }
    }
}
