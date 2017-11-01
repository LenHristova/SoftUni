using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RageQuit
{
    class StartUp
    {
        static void Main()
        {
            string input = Console.ReadLine();
            string[] strings = Regex.Split(input, @"[0-9]+")
                .Where(s => s != "")
                .ToArray();

            string[] stringsRepeats = Regex.Matches(input, @"[0-9]+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strings.Length; i++)
            {
                int repeats = int.Parse(stringsRepeats[i]);
                for (int j = 0; j < repeats; j++)
                {
                    sb.Append(strings[i].ToUpper());
                }
            }

            int uniqueSymbolsCount = string.Join("", 
                sb.ToString().ToLower().Distinct())
                .Length;

            Console.WriteLine($"Unique symbols used: {uniqueSymbolsCount}");
            Console.WriteLine(sb);
        }
    }
}
