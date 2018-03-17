using System;

namespace BashSoft.Exceptions
{
    public class DataAlreadyInitializedException : Exception
    {
        private const string DATA_ALREADY_INITIALIZED = "Data is already initialized!";

        public DataAlreadyInitializedException() : base(DATA_ALREADY_INITIALIZED) { }

        public DataAlreadyInitializedException(string message) : base(message) { }
    }
}