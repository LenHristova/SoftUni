using System;
using System.Collections.Generic;
using System.Linq;


class StartUp
{
    static void Main()
    {
        var input = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var elementsEnqueueCount = input[0];
        var elementsDequeueCount = input[1];
        var searchedElement = input[2];

        var numbersArray = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var numbersQueue = new Queue<int>();

        for (int i = 0; i < elementsEnqueueCount; i++)
        {
            numbersQueue.Enqueue(numbersArray[i]);
        }

        for (int i = 0; i < elementsDequeueCount; i++)
        {
            numbersQueue.Dequeue();
        }

        if (numbersQueue.Count == 0)
        {
            Console.WriteLine(0);
        }
        else if (numbersQueue.Contains(searchedElement))
        {
            Console.WriteLine("true");
        }
        else
        {
            Console.WriteLine(numbersQueue.Min());
        }
    }
}