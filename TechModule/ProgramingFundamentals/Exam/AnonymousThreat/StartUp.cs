using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousThreat
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
                if (input == "3:1")
                    break;

                string[] commandArgs = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                string command = commandArgs[0];

                switch (command)
                {
                    case "merge":
                        Merge(stringsList, commandArgs);
                        break;
                    case "divide":
                        Divide(stringsList, commandArgs);
                        break;
                }
            }

            Console.WriteLine(string.Join(" ", stringsList));
        }

        private static void Divide(List<string> stringsList, string[] commandArgs)
        {
            int index = int.Parse(commandArgs[1]);
            int partitions = int.Parse(commandArgs[2]);
            string str = stringsList[index];

            stringsList
                .RemoveAt(index);
            int substringsLength = str.Length / partitions;

            List<string> temp = new List<string>();
            for (int i = 0; i < partitions; i ++)
            {
                string subStr = string.Join("", str.Substring(0, substringsLength));

                if (i == partitions - 1)
                {
                    subStr = string.Join("", str.Take(substringsLength * 2));
                }

                str = str.Remove(0, substringsLength);
                temp.Add(subStr);
            }
            stringsList.InsertRange(index, temp);
        }

        private static void Merge(List<string> stringsList, string[] commandArgs)
        {
            int startIndex = Math.Max(0, int.Parse(commandArgs[1]));
            int endIndex = Math.Min(stringsList.Count - 1, int.Parse(commandArgs[2]));

            string merged = string.Join("", stringsList
                .Skip(startIndex)
                .Take(endIndex - startIndex +1));

            try
            {
                stringsList.RemoveRange(startIndex, endIndex - startIndex + 1);
            }
            catch (Exception e)
            {
                return;
            }
            
            stringsList.Insert(startIndex, merged);
        }
    }
}
