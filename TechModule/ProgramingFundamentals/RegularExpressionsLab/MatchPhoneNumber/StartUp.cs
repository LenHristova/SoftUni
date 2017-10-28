using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MatchPhoneNumber
{
    class StartUp
    {
        static void Main()
        {
            string regex = @"\+359([\s?\-?])2\1\d{3}\1\d{4}\b";
            string phones = Console.ReadLine();

            string[] machedPhones = Regex.Matches(phones, regex)
                .Cast<Match>()
                .Select(a => a.Value)
                .ToArray();

            Console.WriteLine(string.Join(", ", machedPhones));
        }
    }
}
