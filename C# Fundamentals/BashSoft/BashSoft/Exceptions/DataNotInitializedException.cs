using System;

namespace BashSoft.Exceptions
{
    public class DataNotInitializedException : Exception
    {
        private const string DATA_NOT_INITIALIZED = "The data structure must be initialized first in order to make any operations with it.";

        public DataNotInitializedException() : base(DATA_NOT_INITIALIZED) { }

        public DataNotInitializedException(string message) : base(message) { }
    }
}