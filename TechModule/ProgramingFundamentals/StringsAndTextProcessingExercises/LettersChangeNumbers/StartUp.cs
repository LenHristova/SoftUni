using System;
using System.Linq;

namespace LettersChangeNumbers
{
    class StartUp
    {
        static void Main()
        {
            string[] args = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            decimal sum = 0L;
            foreach (var arg in args)
            {
                if (char.IsLetter(arg.First()) &&
                    char.IsLetter(arg.Last()))
                {
                    char beforeNum = arg.First();
                    char afterNum = arg.Last();
                    decimal num = decimal.Parse(string.Join("",
                        arg
                            .Remove(arg.Length - 1, 1)
                            .Skip(1)));

                    if (char.IsUpper(beforeNum))
                    {
                        num /= beforeNum - 64;
                    }
                    else if (char.IsLower(beforeNum))
                    {
                        num *= beforeNum - 96;
                    }

                    if (char.IsUpper(afterNum))
                    {
                        num -= afterNum - 64;
                    }
                    else if (char.IsLower(afterNum))
                    {
                        num += afterNum - 96;
                    }

                    sum += num;
                }
            }

            Console.WriteLine($"{sum:F2}");
        }
    }
}