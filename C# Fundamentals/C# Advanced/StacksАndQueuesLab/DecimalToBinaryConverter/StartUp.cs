using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var inputDecimal = int.Parse(Console.ReadLine());

        if (inputDecimal == 0)
        {
            Console.WriteLine(0);
            return;
        }

        var stack = new Stack<int>();

        while (inputDecimal > 0)
        {
            stack.Push(inputDecimal % 2);
            inputDecimal /= 2;
        }

        foreach (var num in stack)
        {
            Console.Write(num);
        }

        Console.WriteLine();
    }
}