using System;

namespace P04_OnlineRadioDatabase.Exceptions
{
    public class InvalidSongException : Exception
    {
        public override string Message => "Invalid song.";
    }
}