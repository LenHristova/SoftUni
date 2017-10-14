using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        int[] numbers = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        int k = numbers.Length / 4;
        int[] sideArr = numbers.Take(k).Reverse()
            .Concat(numbers.Skip(3 * k).Reverse())
            .ToArray();
        int[] middleArr = numbers.Skip(k).Take(2 * k).ToArray();

        int[] result = new int[2 * k]
            .Select((n, index) => sideArr[index] + middleArr[index])
            .ToArray();
        Console.WriteLine(string.Join(" ", result));
    }
}