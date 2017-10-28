using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MatchHexadecimalNumbers
{
    class StartUp
    {
        static void Main()
        {
            string pattern = @"\b(?:0x)?[0-9A-F]+\b";
            string input = Console.ReadLine();

            string[] hexadecimalNumbers = Regex.Matches(input, pattern)
                                                .Cast<Match>()
                                                .Select(x => x.Value)
                                                .ToArray();

            Console.WriteLine(string.Join(" ", hexadecimalNumbers));
        }
    }
}
