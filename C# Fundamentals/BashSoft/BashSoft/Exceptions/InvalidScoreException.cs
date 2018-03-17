using System;

namespace BashSoft.Exceptions
{
    class InvalidScoreException : Exception
    {
        private const string INVALID_SCORE = "The score for the given course is greater or smaller than the possible.";

        public InvalidScoreException() : base(INVALID_SCORE) { }

        public InvalidScoreException(string message) : base(message) { }
    }
}
