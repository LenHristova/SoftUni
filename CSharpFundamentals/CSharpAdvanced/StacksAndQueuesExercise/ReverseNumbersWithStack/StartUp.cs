using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var numbersStack = new Stack<int>(numbers);

        while (numbersStack.Count > 0)
        {
            Console.Write(numbersStack.Pop() + " ");
        }

        Console.WriteLine();
    }
}