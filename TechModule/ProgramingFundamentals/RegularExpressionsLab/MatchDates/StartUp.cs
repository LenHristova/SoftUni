﻿using System;
using System.Text.RegularExpressions;

namespace MatchDates
{
    class StartUp
    {
        static void Main()
        {
            string regex = @"\b(?<day>\d{2})([-.\/])(?<month>[A-Z][a-z]{2})\1(?<year>\d{4})\b";
            string input = Console.ReadLine();
            MatchCollection matches = Regex.Matches(input, regex);

            foreach (Match match in matches)
            {
                string day = match.Groups["day"].Value;
                string month = match.Groups["month"].Value;
                string year = match.Groups["year"].Value;

                Console.WriteLine($"Day: {day}, Month: {month}, Year: {year}");
            }      
        }
    }
}