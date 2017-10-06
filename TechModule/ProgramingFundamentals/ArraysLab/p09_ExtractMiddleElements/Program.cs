using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int[] middleElements = GetMiddleElements(numbers);
        Console.WriteLine($"{{ {string.Join(", ", middleElements)} }}");
    }

    private static int[] GetMiddleElements(int[] numbers)
    {
        int elementsCount = 1;
        bool isEven = numbers.Length % 2 == 0;
        if (isEven)
        {
            elementsCount = 2;
        }
        else if (!isEven && numbers.Length != 1)
        {
            elementsCount = 3;
        }
        return GetElements(numbers, elementsCount);
    }

    private static int[] GetElements(int[] numbers, int elementsCount)
    {
        return numbers
            .Skip((numbers.Length - 2) / 2)
            .Take(elementsCount)
            .ToArray();
    }
}

