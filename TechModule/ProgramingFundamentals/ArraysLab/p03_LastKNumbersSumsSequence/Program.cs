using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int numsCount = int.Parse(Console.ReadLine());
        int numsSumCount = int.Parse(Console.ReadLine());

        long currentNum = 1;
        List<long> sumNumbers = new List<long>();
        sumNumbers.Add(currentNum);

        for (int i = 0; i < numsCount; i++)
        {
            Console.Write(currentNum + " ");
            currentNum = sumNumbers.Sum();

            sumNumbers.Add(currentNum);
            if (sumNumbers.Count > numsSumCount)
            {
                sumNumbers.RemoveAt(0);
            }
        }
        Console.WriteLine();
    }
}

