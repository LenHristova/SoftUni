using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandInterpreter
{
    class StartUp
    {
        static void Main()
        {
            List<string> stringsList = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
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
                    case "reverse":
                        stringsList = ReverseList(stringsList, commandArgs);
                        break;
                    case "sort":
                        stringsList = SortList(stringsList, commandArgs);
                        break;
                    case "rollLeft":
                        stringsList = RollLeftList(stringsList, commandArgs);
                        break;
                    case "rollRight":
                        stringsList = RollRightList(stringsList, commandArgs);
                        break;
                }
            }

            Console.WriteLine($"[{string.Join(", ", stringsList)}]");
        }

        private static List<string> RollRightList(List<string> stringsList, string[] commandArgs)
        {
            long rotationsCount = long.Parse(commandArgs[1]);

            if (rotationsCount < 0)
            {
                Console.WriteLine("Invalid input parameters.");
                return stringsList;
            }

            rotationsCount %= stringsList.Count;

            for (long rotations = 0; rotations < rotationsCount; rotations++)
            {
                string oldLast = stringsList[stringsList.Count - 1];
                for (int i = stringsList.Count - 1; i > 0; i--)
                {
                    stringsList[i] = stringsList[i - 1];
                }
                stringsList[0] = oldLast;
            }

            return stringsList;
        }

        private static List<string> RollLeftList(List<string> stringsList, string[] commandArgs)
        {
            long rotationsCount = long.Parse(commandArgs[1]);

            if (rotationsCount < 0)
            {
                Console.WriteLine("Invalid input parameters.");
                return stringsList;
            }

            rotationsCount %= stringsList.Count;

            for (long rotations = 0; rotations < rotationsCount; rotations++)
            {
                string oldFirst = stringsList[0];
                for (int i = 0; i < stringsList.Count - 1; i++)
                {
                    stringsList[i] = stringsList[i + 1];
                }
                stringsList[stringsList.Count - 1] = oldFirst;
            }

            return stringsList;
        }

        private static List<string> SortList(List<string> stringsList, string[] commandArgs)
        {
            int start = int.Parse(commandArgs[2]);
            int count = int.Parse(commandArgs[4]);

            if (IsValidInput(start, count, stringsList))
            {
                return stringsList.Take(start)
                                .Concat(stringsList.Skip(start).Take(count).OrderBy(x => x))
                                .Concat(stringsList.Skip(start + count))
                                .ToList();
            }

            Console.WriteLine("Invalid input parameters.");
            return stringsList;
        }

        private static List<string> ReverseList(List<string> stringsList, string[] commandArgs)
        {
            int start = int.Parse(commandArgs[2]);
            int count = int.Parse(commandArgs[4]);

            if (IsValidInput(start, count, stringsList))
            {
                return stringsList.Take(start)
                    .Concat(stringsList.Skip(start).Take(count).Reverse())
                    .Concat(stringsList.Skip(start + count))
                    .ToList();
            }

            Console.WriteLine("Invalid input parameters.");
            return stringsList;
        }

        private static bool IsValidInput(int start, int count, List<string> stringsList)
        {
            return start >= 0
                   && count >= 0
                   && start < stringsList.Count
                   && (long)start + count <= stringsList.Count;
        }
    }
}