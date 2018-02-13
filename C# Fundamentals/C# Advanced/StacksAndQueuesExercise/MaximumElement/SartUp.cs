using System;
using System.Collections.Generic;

class StartUp
{
    private static Stack<int> stack = new Stack<int>();
    private static Stack<int> maxStack = new Stack<int>();

    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        maxStack.Push(int.MinValue);

        for (var i = 0; i < n; i++)
        {
            var query = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            switch (query[0])
            {
                case "1":
                    PushNumber(int.Parse(query[1]));
                    break;
                case "2":
                    PopNumber(stack.Pop());
                    break;
                case "3":
                    if (stack.Count > 0)
                    {
                        Console.WriteLine(maxStack.Peek());
                    }
                    break;
            }
        }
    }

    private static void PopNumber(int pop)
    {
        if (pop == maxStack.Peek())
        {
            maxStack.Pop();
        }
    }

    private static void PushNumber(int num)
    {
        stack.Push(num);
        if (num >= maxStack.Peek())
        {
            maxStack.Push(num);
        }
    }
}