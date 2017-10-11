using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int rotationsCount = int.Parse(Console.ReadLine());
        int[] sum = Sum(numbers, rotationsCount);
        Console.WriteLine(string.Join(" ", sum));
    }

    private static int[] Sum(int[] numbers, int rotationsCount)
    {
        int[] sum = new int[numbers.Length];
        int allCalculations = rotationsCount * numbers.Length;

        for (int i = 0; i < allCalculations; i++)
        {
            if (i % sum.Length == 0)
            {
                Rotate(numbers);
            }

            sum[i % sum.Length] += numbers[i % numbers.Length];
        }

        return sum;
    }

    static void Rotate(int[] arr)
    {
        int lastElement = arr[arr.Length - 1];
        for (int i = arr.Length - 1; i > 0; i--)
        {
            arr[i] = arr[i - 1];
        }
        arr[0] = lastElement;
    }
}