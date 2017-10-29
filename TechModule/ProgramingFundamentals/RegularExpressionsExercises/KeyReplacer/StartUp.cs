using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KeyReplacer
{
    class StartUp
    {
        static void Main()
        {
            string startPatern = @"^[A-Za-z]+[|<\\]";
            string endPatern = @"[|<\\][A-Za-z]+$";

            string startEnd = Console.ReadLine();

            if (Regex.IsMatch(startEnd, startPatern) && Regex.IsMatch(startEnd, endPatern))
            {
                string start = Regex.Match(startEnd, startPatern).Value.TrimEnd('|', '<', '\\');
                string end = Regex.Match(startEnd, endPatern).Value.TrimStart('|', '<', '\\');

                string[] phrases = Regex.Split(Console.ReadLine(), $@"{end}")
                    .Where(ph => ph != "")
                    .ToArray();

                StringBuilder sb = new StringBuilder();
                foreach (string phrase in phrases)
                {
                    sb.Append(Regex.Match(phrase, $@"(?<={start}).*").Value);
                }

                Console.WriteLine(sb.ToString() == string.Empty ? "Empty result" : sb.ToString());
            }
        }
    }
}