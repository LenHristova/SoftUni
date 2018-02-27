using System;
using System.Linq;
using System.Text.RegularExpressions;
using P04_OnlineRadioDatabase.Exceptions;

namespace P04_OnlineRadioDatabase
{
    public static class Validator
    {
        private const int VALID_SONG_ARGS_COUNT = 3;
        private const string VALID_SONG_LENGTH_PATTERN = @"^\d+:\d+$";

        public const int MIN_ARTIST_NAME_LENGTH = 3;
        public const int MAX_ARTIST_NAME_LENGTH = 20;

        public const int MIN_SONG_NAME_LENGTH = 3;
        public const int MAX_SONG_NAME_LENGTH = 30;

        private const int MAX_SONG_MINUTES = 14;
        private const int MAX_SONG_SECONDS = 59;


        private static void ValidateStringLength(string value, int minLength, int maxLength)
        {
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new InvalidSongException();
            }
        }

        public static void ValidateArtistName(string value)
        {
            try
            {
                ValidateStringLength(value, MIN_ARTIST_NAME_LENGTH, MAX_ARTIST_NAME_LENGTH);
            }
            catch (InvalidSongException)
            {
                throw new InvalidArtistNameException();
            }
        }

        public static void ValidateSongName(string value)
        {
            try
            {
                ValidateStringLength(value, MIN_SONG_NAME_LENGTH, MAX_SONG_NAME_LENGTH);
            }
            catch (InvalidSongException)
            {
                throw new InvalidSongNameException();
            }
        }

        public static void ValidateTimeSpan(string value)
        {
            if (!Regex.IsMatch(value, VALID_SONG_LENGTH_PATTERN))
            {
                throw new InvalidSongLengthException();
            }

            var timeSpan = value
                .Split(':', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            if (timeSpan[0] > MAX_SONG_MINUTES)
            {
                throw new InvalidSongMinutesException();
            }

            if (timeSpan[1] > MAX_SONG_SECONDS)
            {
                throw new InvalidSongSecondsException();
            }
        }

        public static void ValidateSong(string[] songInfo)
        {
            if (songInfo.Length < VALID_SONG_ARGS_COUNT)
            {
                throw new InvalidSongException();
            }
        }
    }
}