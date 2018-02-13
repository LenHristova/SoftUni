using System;
using System.Collections.Generic;

class StartUp
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var stack = new Stack<long>();
            stack.Push(0);
            stack.Push(1);

            for (int i = 2; i <= n; i++)
            {
                var prevFib = stack.Peek();
                var fib = stack.Pop() + stack.Pop();
                stack.Push(prevFib);
                stack.Push(fib);
            }

            Console.WriteLine(stack.Peek());
        }
    }