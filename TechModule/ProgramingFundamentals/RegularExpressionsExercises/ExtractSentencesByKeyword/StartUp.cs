using System;
using System.Text.RegularExpressions;

namespace ExtractSentencesByKeyword
{
    class StartUp
    {
        static void Main()
        {
            string word = Console.ReadLine();
            string pattern = @"[A-Z][^.!?]*\b" + word + @"\b[^.!?]*";
            string text = Console.ReadLine();

            MatchCollection phrasesMatchCollection = Regex.Matches(text, pattern);

            foreach (Match match in phrasesMatchCollection)
            {
                Console.WriteLine(match.Value);
            }
        }
    }
}