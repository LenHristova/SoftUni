using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_ArrayManipulator
{
    class ArrayManipulator
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "print")
                {
                    break;
                }

                string[] tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];

                ExecuteCommand(numbers, tokens, command);
            }
            Console.WriteLine($"[{string.Join(", ", numbers)}]");
        }

        private static void ExecuteCommand(List<int> numbers, string[] tokens, string command)
        {
            switch (command)
            {
                case "add":
                    AddElement(numbers, tokens);
                    break;
                case "addMany":
                    AddManyElements(numbers, tokens);
                    break;
                case "contains":
                    PrintElementIfContains(numbers, tokens);
                    break;
                case "remove":
                    RemoveElement(numbers, tokens);
                    break;
                case "shift":
                    Shift(numbers, tokens);
                    break;
                case "sumPairs":
                    SumPairs(numbers);
                    break;
            }
        }

        private static void SumPairs(List<int> numbers)
        {
            for (int pos = 0; pos < numbers.Count -1; pos++)
            {
                    numbers[pos] = numbers[pos] + numbers[pos + 1];
                    numbers.RemoveAt(pos + 1);
            }
        }

        private static void Shift(List<int> numbers, string[] tokens)
        {
            int position = int.Parse(tokens[1]) % numbers.Count;
            numbers.AddRange(numbers.Take(position).ToList());
            numbers.RemoveRange(0, position);
        }

        private static void RemoveElement(List<int> numbers, string[] tokens)
        {
            int index = int.Parse(tokens[1]);
            numbers.RemoveAt(index);
        }

        private static void PrintElementIfContains(List<int> numbers, string[] tokens)
        {
            int element = int.Parse(tokens[1]);
            Console.WriteLine(numbers.IndexOf(element));
        }

        private static void AddManyElements(List<int> numbers, string[] tokens)
        {
            int index = int.Parse(tokens[1]);
            int[] elements = tokens
                .Skip(2)
                .Select(int.Parse)
                .ToArray();
            numbers.InsertRange(index, elements);
        }

        private static void AddElement(List<int> numbers, string[] tokens)
        {
            int index = int.Parse(tokens[1]);
            int element = int.Parse(tokens[2]);
            numbers.Insert(index, element);
        }
    }
}
