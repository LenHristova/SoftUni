using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Regexmon
{
    class StartUp
    {
        static void Main()
        {
            string text = Console.ReadLine();

            while (true)
            {
                string didiPattern = @"[^A-Za-z-]+";

                string didiMatch = Regex.Match(text, didiPattern).Value;
                if (didiMatch == string.Empty)
                {
                    break;
                }
                Console.WriteLine(didiMatch);
                text = Regex.Match(text, $@"(?<={Regex.Escape(didiMatch)}).*").Value;

                string bojoPattern = @"[A-Za-z]+-[A-Za-z]+";
                string bojoMatch = Regex.Match(text, bojoPattern).Value;
                if (bojoMatch == string.Empty)
                {
                    break;
                }
                Console.WriteLine(bojoMatch);
                text = Regex.Match(text, $@"(?<={Regex.Escape(bojoMatch)}).*").Value;

            }
        }
    }
}
