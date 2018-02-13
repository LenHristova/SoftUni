using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var expression = Console.ReadLine();
        var result = new List<string>();

        var stack = new Stack<int>();

        for (var pos = 0; pos < expression.Length; pos++)
        {
            switch (expression[pos])
            {
                case '(':
                    stack.Push(pos);
                    break;
                case ')':
                    var openBrachetIndex = stack.Pop();

                    result.Add(expression.Substring(openBrachetIndex, pos-openBrachetIndex + 1));
                    break;
            }
        }

        if (stack.Count > 0)
        {
            Console.WriteLine("Invalid expression!");
        }
        else
        {
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }
    }
}