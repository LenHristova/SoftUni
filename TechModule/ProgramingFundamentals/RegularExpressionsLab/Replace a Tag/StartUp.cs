using System;
using System.Text.RegularExpressions;

namespace Replace_a_Tag
{
    class StartUp
    {
        static void Main()
        {
            string regex = @"<a.*?href.*?=(.*)>(.*?)<\/a>";
            string replacment = @"[URL href=$1]$2[/URL]";

            string input = Console.ReadLine();
            while (input != "end")
            {
                string output = Regex.Replace(input, regex, replacment);

                Console.WriteLine(output);
                input = Console.ReadLine();
            }
        }
    }
}