using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnonymousVox
{
    class StartUp
    {
        static void Main()
        {
            string pattern = @"(?<start>[A-Za-z]+)(?<placeholder>.+)(?<end>\1)";
            string text = Console.ReadLine();

            var groups = Regex.Matches(text, pattern)
                .Cast<Match>()
                .Select(m => m.Groups);

            string[] placeholder = groups
                .Select(gr => gr["placeholder"].Value)
                .ToArray();
            string[] startEnd = groups
                .Select(gr => gr["start"].Value)
                .ToArray();
            string[] values = Console.ReadLine()
                .Split(new[] {'{', '}'}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < placeholder.Length; i++)
            {
                text = text.Replace(startEnd[i] + placeholder[i] + startEnd[i],
                    startEnd[i] + values[i] + startEnd[i]);
            }           

            Console.WriteLine(text);
        }
    }
}