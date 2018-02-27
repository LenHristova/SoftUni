using System;
using System.Globalization;

namespace P04_OnlineRadioDatabase.Models
{
    public class Song
    {
        private string _artistName;
        private string _songName;

        private string ArtistName
        {
            set
            {
                Validator.ValidateArtistName(value);
                _artistName = value;
            }
        }

        private string SongName
        {
            set
            {
                Validator.ValidateSongName(value);
                _songName = value;
            }
        }

        public TimeSpan SongLength { get; private set; }

        private string SongLengthStr
        {
            set
            {
                Validator.ValidateTimeSpan(value);
                SongLength = TimeSpan.ParseExact(value, @"m\:s", CultureInfo.InvariantCulture, TimeSpanStyles.None);
            }
        }

        public Song(string input)
        {
            var songInfo = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
            Validator.ValidateSong(songInfo);

            ArtistName = songInfo[0].Trim();
            SongName = songInfo[1].Trim();
            SongLengthStr = songInfo[2].Trim();
        }
    }
}