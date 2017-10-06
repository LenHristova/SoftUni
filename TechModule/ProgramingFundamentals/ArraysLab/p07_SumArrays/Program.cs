using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int[] arr1 = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        int[] arr2 = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int[] sumArr = new int[Math.Max(arr1.Length, arr2.Length)];
        for (int i = 0; i < sumArr.Length; i++)
        {
            sumArr[i] = arr1[i % arr1.Length] + arr2[i % arr2.Length];
        }
        Console.WriteLine(string.Join(" ", sumArr));
    }
}

