using System;

namespace BashSoft.Exceptions
{
    public class InvalidCommandException : Exception
    {
        private const string INVALID_COMMAND = "The command '{0}' is invalid";

        public InvalidCommandException(string entry)
            : base(string.Format(INVALID_COMMAND, entry)) { }
    }
}
