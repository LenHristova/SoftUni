using System;

namespace BashSoft.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string INVALID_PATH =
            "The folder/file you are trying to access at the current address, does not exist.";

        public InvalidPathException() : base(INVALID_PATH) { }

        public InvalidPathException(string message) : base(message) { }
    }
}
