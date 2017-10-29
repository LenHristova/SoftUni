using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ValidUsernames
{
    class StartUp
    {
        static void Main()
        {
            string[] usernames = Regex.Split(Console.ReadLine(), @"[\s+\/\\(),]")
                .Where(u => u !="")
                .ToArray();

            string[] validUsernames = usernames
                .Where(u => Regex.IsMatch(u, @"^[A-Za-z]\w{2,24}$"))
                .Select(u => Regex.Match(u, @"^[A-Za-z]\w{2,24}$").Value)
                .ToArray();

            int sum = 0;
            string res = String.Empty;
            for (int i = 0; i < validUsernames.Length-1; i++)
            {
                int currSum = validUsernames[i].Length + validUsernames[i + 1].Length;
                if (currSum <= sum)
                {
                    continue;
                }

                sum = currSum;
                res = validUsernames[i] + Environment.NewLine + validUsernames[i + 1];
            }

            Console.WriteLine(res);
        }
    }
}