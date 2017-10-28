using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MatchNumbers
{
    class StartUp
    {
        static void Main()
        {
            string regex = @"(^|(?<=\s))-?\d+(\.\d+)?($|(?=\s))";

            string[] numbers = Regex.Matches(Console.ReadLine(), regex)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}