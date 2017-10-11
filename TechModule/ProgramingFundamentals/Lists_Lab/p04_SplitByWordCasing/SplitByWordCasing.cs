using System;
using System.Collections.Generic;
using System.Linq;

namespace p04_SplitByWordCasing
{
    class SplitByWordCasing
    {
        static void Main()
        {
            List<string> allWords = Console.ReadLine()
                .Split(new[] {' ', ',', ';', ':', '.', '!', '(', ')', '"', '\'', '\\', '/', '[', ']'},
                    StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            List<string> lowercaseWords = allWords
                .Where(w => w.ToCharArray().All(ch => char.IsLower(ch)))
                .ToList();
            allWords.RemoveAll(w => w.ToCharArray().All(ch => char.IsLower(ch)));
            List <string> upercaseWords = allWords
                .Where(w => w.ToCharArray().All(ch => char.IsUpper(ch)))
                .ToList();
            allWords.RemoveAll(w => w.ToCharArray().All(ch => char.IsUpper(ch)));
            List<string> mixedcaseWords = allWords.GetRange(0, allWords.Count);
                

            Console.WriteLine($"Lower-case: {string.Join(", ", lowercaseWords) + Environment.NewLine}" +
                              $"Mixed-case: {string.Join(", ", mixedcaseWords) + Environment.NewLine}" +
                              $"Upper-case: {string.Join(", ", upercaseWords)}");
        }
    }
}
