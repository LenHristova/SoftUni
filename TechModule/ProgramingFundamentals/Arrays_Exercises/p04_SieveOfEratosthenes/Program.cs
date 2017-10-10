using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int lastNumOfTheRange = int.Parse(Console.ReadLine());
        bool[] isPrime = new bool[lastNumOfTheRange + 1]
            .Select(b => true)
            .ToArray();

        isPrime[0] = false;
        isPrime[1] = false;

        PrintPrimes(isPrime);
    }

    static void PrintPrimes(bool[] isPrime)
    {
        for (int currentNum = 2; currentNum < isPrime.Length; currentNum++)
        {
            if (!isPrime[currentNum])
            {
                continue;
            }

            Console.Write(currentNum + " ");
            for (int pos = currentNum * 2; pos < isPrime.Length; pos += currentNum)
            {
                isPrime[pos] = false;
            }
        }
        Console.WriteLine();
    }
}

