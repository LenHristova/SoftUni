using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        Console.WriteLine(string.Join(" ", LongestSubsequenceOfEqualNumbers(numbers)));
    }

    private static List<int> LongestSubsequenceOfEqualNumbers(List<int> numbers)
    {
        var maxNumber = numbers[0];
        var maxCounter = 1;

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            var number = numbers[i];
            var counter = 1;

            while (i < numbers.Count - 1 && number == numbers[i + 1])
            {
                counter++;
                i++;
            }

            if (maxCounter < counter)
            {
                maxNumber = number;
                maxCounter = counter;
            }
        }

        return new int[maxCounter]
            .Select(n => maxNumber)
            .ToList();
    }
}