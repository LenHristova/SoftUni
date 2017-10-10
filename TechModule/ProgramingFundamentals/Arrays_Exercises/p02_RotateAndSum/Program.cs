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

        int[] sum = new int[numbers.Length];
        int allCalculations = rotationsCount * numbers.Length;


        for (int i = 0; i < allCalculations; i++)
        {
            if (i % sum.Length == 0)
            {
                RotateList(numbers);
            }

            sum[i % sum.Length] += numbers[i % numbers.Length];
        }
        Console.WriteLine(string.Join(" ", sum));
    }

    static int[] RotateList(int[] arr)
    {
        int lastElement = arr[arr.Length - 1];
        for (int i = arr.Length - 1; i > 0; i--)
        {
            arr[i] = arr[i - 1];
        }
        arr[0] = lastElement;
        return arr;
    }
}

