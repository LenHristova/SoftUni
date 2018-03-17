using System;

namespace BashSoft.Exceptions
{
    public class InvalidNumberException : Exception
    {
        private const string UNABLE_TO_PARSE_NUMBER = "The sequence you've written is not a valid number.";

        public InvalidNumberException() : base(UNABLE_TO_PARSE_NUMBER) { }

        public InvalidNumberException(string message) : base(message) { }
    }
}
