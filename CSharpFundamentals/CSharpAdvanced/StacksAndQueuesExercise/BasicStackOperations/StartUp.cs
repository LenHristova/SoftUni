using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    //You will be given an integer N representing the number of elements to push onto the stack, 
    //an integer S representing the number of elements to pop from the stack 
    //and finally an integer X, an element that you should look for in the stack.
    //If it’s found, print “true” on the console. If it isn’t,
    //print the smallest element currently present in the stack.

    static void Main()
    {

        var input = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var n = input[0];
        var s = input[1];
        var x = input[2];

        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var numbersStack = new Stack<int>(numbers.Take(n));

        for (var i = 0; i < s; i++)
        {
            numbersStack.Pop();
        }

        Console.WriteLine(numbersStack.Contains(x) ?
            "true" :
            numbersStack
                .OrderBy(num => num)
                .FirstOrDefault()
                .ToString());
    }
}