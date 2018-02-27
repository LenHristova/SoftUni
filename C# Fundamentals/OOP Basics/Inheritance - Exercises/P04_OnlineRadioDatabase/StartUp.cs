using System;
using System.Collections.Generic;
using System.Linq;
using P04_OnlineRadioDatabase.Exceptions;
using P04_OnlineRadioDatabase.Models;

namespace P04_OnlineRadioDatabase
{
    class StartUp
    {
        static void Main()
        {
            var playlist = new List<Song>();

            var songsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < songsCount; i++)
            {
                TryToAddSong(playlist);
            }

            PrintPlaylistInfo(playlist);
        }

        private static void PrintPlaylistInfo(List<Song> playlist)
        {
            Console.WriteLine($"Songs added: {playlist.Count}");

            var playlistLength = playlist
                .Select(s => s.SongLength)
                .Aggregate(new TimeSpan(0, 0, 0), (current, timeSpan) => current + timeSpan);

            Console.WriteLine("Playlist length: " +
                              $"{playlistLength.Hours}h " +
                              $"{playlistLength.Minutes}m " +
                              $"{playlistLength.Seconds}s");
        }

        private static void TryToAddSong(List<Song> playlist)
        {
            try
            {
                playlist.Add(new Song(Console.ReadLine()));
                Console.WriteLine("Song added.");
            }
            catch (InvalidArtistNameException iane)
            {
                Console.WriteLine(iane.Message);
            }
            catch (InvalidSongNameException isne)
            {
                Console.WriteLine(isne.Message);
            }
            catch (InvalidSongException ise)
            {
                Console.WriteLine(ise.Message);
            }
            catch (InvalidSongMinutesException isme)
            {
                Console.WriteLine(isme.Message);
            }
            catch (InvalidSongSecondsException isse)
            {
                Console.WriteLine(isse.Message);
            }
            catch (InvalidSongLengthException isle)
            {
                Console.WriteLine(isle.Message);
            }
        }
    }
}