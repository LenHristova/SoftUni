using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UseYourChainsBuddy
{
    class StartUp
    {
        static void Main()
        {
            //Get info from HTML input, needed info is between <p> and </p>
            string[] info = Regex.Matches(Console.ReadLine(), @"(?<=<p>).*?(?=<\/p>)")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            for (int i = 0; i < info.Length; i++)
            {
                info[i] = Regex.Replace(info[i], @"[^a-z0-9]", " ");

                info[i] = Regex.Replace(info[i], @"\s+", " ");

                info[i] = string.Join("", info[i]
                    .Select(Decode));
            }


            Console.WriteLine(string.Join("", info));
        }

        static char Decode(char ch)
        {
            if (Regex.IsMatch(ch.ToString(), @"([a-m])"))
            {
                ch = (char) (ch + 13);
            }
            else if (Regex.IsMatch(ch.ToString(), @"([n-z])"))
            {
                ch = (char) (ch - 13);
            }

            return ch;
        }
    }
}
