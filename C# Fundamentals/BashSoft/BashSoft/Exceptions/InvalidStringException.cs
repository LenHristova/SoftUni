using System;

namespace BashSoft.Exceptions
{
    public class InvalidStringException:Exception
    {
        private const string NULL_OR_EMPTY_VALUE = 
            "The value of the variable CAN NOT be null or empty!";

        public InvalidStringException() : base(NULL_OR_EMPTY_VALUE) { }

        public InvalidStringException(string message) : base(message) { }
    }
}
