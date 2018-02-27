using System;

namespace P04_OnlineRadioDatabase.Exceptions
{
    public class InvalidSongLengthException : Exception
    {
        public override string Message => "Invalid song length.";
    }
}