using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CameraView
{
    class StartUp
    {
        static void Main()
        {
            string pattern = @"\|<\w+";

            int[] takeSkipInts = Regex.Split(Console.ReadLine(), @"\D")
                .Select(int.Parse)
                .ToArray();

            string input = Console.ReadLine();
            MatchCollection camerasMatches = Regex.Matches(input, pattern);

            int skiped = takeSkipInts[0] + 2;
            int taked = takeSkipInts[1];

            string[] cameras = camerasMatches
                .Cast<Match>()
                .Select(m => string.Join("", m.Value.Skip(skiped).Take(taked)))
                .ToArray();

            Console.WriteLine(string.Join(", ", cameras));
        }
    }
}