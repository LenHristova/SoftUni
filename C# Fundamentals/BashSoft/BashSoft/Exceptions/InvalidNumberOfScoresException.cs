using System;

namespace BashSoft.Exceptions
{
    public class InvalidNumberOfScoresException : Exception
    {
        private const string INVALID_NUMBER_OF_SCORES = "The number of scores for the given course is greater than the possible.";

        public InvalidNumberOfScoresException() : base(INVALID_NUMBER_OF_SCORES) { }

        public InvalidNumberOfScoresException(string message) : base(message) { }
    }
}
