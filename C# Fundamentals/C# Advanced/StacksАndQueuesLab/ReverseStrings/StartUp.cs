using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var stack = new Stack<char>(Console.ReadLine());

        while (stack.Count > 0)
        {
            Console.Write(stack.Pop());
        }

        Console.WriteLine();
    }
}