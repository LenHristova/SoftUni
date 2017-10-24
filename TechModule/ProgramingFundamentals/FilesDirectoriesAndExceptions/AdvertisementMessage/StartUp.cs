using System;
using System.IO;

namespace AdvertisementMessage
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            var phrases = File.ReadAllLines(@"..\..\phrases.txt");
            var events = File.ReadAllLines(@"..\..\events.txt");
            var authors = File.ReadAllLines(@"..\..\authors.txt");
            var cities = File.ReadAllLines(@"..\..\cities.txt");

            var rnd = new Random();
            var input = int.Parse(File.ReadAllText(@"..\..\input.txt"));
            for (var currAdv = 0; currAdv < input; currAdv++)
            {
                var currPhrase = phrases[rnd.Next(0, phrases.Length)];
                var currEvent = events[rnd.Next(0, events.Length)];
                var currAuthor = authors[rnd.Next(0, authors.Length)];
                var currCity = cities[rnd.Next(0, cities.Length)];

                File.AppendAllText(@"..\..\output.txt",
                    $"{currPhrase} {currEvent} {currAuthor} - {currCity}{Environment.NewLine}");
            }
        }
    }
}
