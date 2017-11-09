using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ArrayManipulator
{
    class StartUp
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                    break;

                string[] commandArgs = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string command = commandArgs[0];

                switch (command)
                {
                    case "exchange":
                        numbers = ExchangeList(numbers, commandArgs);
                        break;
                    case "max":
                        PrintMax(numbers, commandArgs);
                        break;
                    case "min":
                        PrintMin(numbers, commandArgs);
                        break;
                    case "first":
                        PrintFirst(numbers, commandArgs);
                        break;
                    case "last":
                        PrintLast(numbers, commandArgs);
                        break;
                }
            }

            Console.WriteLine($"[{string.Join(", ", numbers)}]");
        }

        private static void PrintMin(List<int> numbers, string[] commandArgs)
        {
            List<int> min = new List<int>();
            if (commandArgs[1] == "odd")
            {
                min.AddRange(numbers
                    .Where(n => n % 2 != 0)
                    .OrderBy(n => n)
                    .ToList());
            }
            else
            {
                min.AddRange(numbers
                    .Where(n => n % 2 == 0)
                    .OrderBy(n => n)
                    .ToList());
            }
            if (min.Count == 0)
            {
                Console.WriteLine("No matches");
                return;
            }

            Console.WriteLine(numbers.LastIndexOf(min.First()));
        }

        private static void PrintLast(List<int> numbers, string[] commandArgs)
        {
            int count = int.Parse(commandArgs[1]);
            if (count > numbers.Count)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            if (commandArgs[2] == "odd")
            {
                List<int> list = numbers
                    .Where(n => n % 2 != 0)
                    .Reverse()
                    .Take(count)
                    .Reverse()
                    .ToList();
                Console.WriteLine($"[{string.Join(", ", list)}]");
            }
            else
            {
                List<int> list = numbers
                    .Where(n => n % 2 == 0)
                    .Reverse()
                    .Take(count)
                    .Reverse()
                    .ToList();
                Console.WriteLine($"[{string.Join(", ", list)}]");
            }
        }

        private static void PrintFirst(List<int> numbers, string[] commandArgs)
        {
            int count = int.Parse(commandArgs[1]);
            if (count > numbers.Count)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            if (commandArgs[2] == "odd")
            {
                List<int> list = numbers
                    .Where(n => n % 2 != 0)
                    .Take(count)
                    .ToList();
                Console.WriteLine($"[{string.Join(", ", list)}]");
            }
            else
            {
                List<int> list = numbers
                    .Where(n => n % 2 == 0)
                    .Take(count)
                    .ToList();
                Console.WriteLine($"[{string.Join(", ", list)}]");
            }
        }

        private static void PrintMax(List<int> numbers, string[] commandArgs)
        {
            List<int> max = new List<int>();
            if (commandArgs[1] == "odd")
            {
                 max.AddRange(numbers
                    .Where(n => n % 2 != 0)
                    .OrderByDescending(n => n)
                    .ToList());
            }
            else
            {
                 max.AddRange(numbers
                    .Where(n => n % 2 == 0)
                    .OrderByDescending(n => n)
                    .ToList());
            }
            if (max.Count == 0)
            {
                Console.WriteLine("No matches");
                return;
            }

            Console.WriteLine(numbers.LastIndexOf(max.First()));
        }

        private static List<int> ExchangeList(List<int> numbers, string[] commandArgs)
        {
            int index = int.Parse(commandArgs[1]);
            if (index < 0 || index >= numbers.Count)
            {
                Console.WriteLine("Invalid index");
                return numbers;
            }

            List<int> exchangeList = numbers.Skip(index + 1).ToList();
            exchangeList.AddRange(numbers.Take(index + 1));
            return exchangeList;
        }
    }
}