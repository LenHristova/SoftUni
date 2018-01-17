using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var input = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);

        var numbers = new Stack<int>(input);

        while (numbers.Count > 0)
        {
            Console.Write(numbers.Pop() + " ");
        }

        Console.WriteLine();
    }
}