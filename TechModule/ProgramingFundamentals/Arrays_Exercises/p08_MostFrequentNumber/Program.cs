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

        int mostFrequentNumber = GetmostFrequentNumber(numbers);
        Console.WriteLine(mostFrequentNumber);
    }

    static int GetmostFrequentNumber(int[] numbers)
    {
        int counter = 1;
        int max = 0;
        int mostFrequentNumber = numbers[0];
        for (int currentNum = 0; currentNum < numbers.Length; currentNum++)
        {
            if (numbers[currentNum] == -1)
            {
                continue;
            }
            for (int comparedNum = currentNum + 1; comparedNum < numbers.Length; comparedNum++)
            {
                if (numbers[comparedNum] == -1)
                {
                    continue;
                }
                if (numbers[currentNum] == numbers[comparedNum])
                {
                    numbers[comparedNum] = -1;
                    counter++;
                }
            }
            if (counter > max)
            {
                max = counter;
                mostFrequentNumber = numbers[currentNum];
            }
            counter = 1;
        }
        return mostFrequentNumber;
    }
}

