using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        int[] numbers = new int[int.Parse(Console.ReadLine())]
            .Select(n => int.Parse(Console.ReadLine()))
            .ToArray();

        Console.WriteLine($"Sum = {numbers.Sum()}\r\n" +
                          $"Min = {numbers.Min()}\r\n" +
                          $"Max = {numbers.Max()}\r\n" +
                          $"Average = {numbers.Average()}");
    }
}
