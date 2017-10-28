using System;
using System.Text.RegularExpressions;

namespace MatchFullName
{
    class StartUp
    {
        static void Main()
        {
            string regex = @"\b[A-Z][a-z]+ [A-Z][a-z]+\b";
            string names = Console.ReadLine();
            var machedNames = Regex.Matches(names, regex);

            foreach (Match machedName in machedNames)
            {
                Console.Write(machedName.Value + " ");
            }

            Console.WriteLine();
        }
    }
}