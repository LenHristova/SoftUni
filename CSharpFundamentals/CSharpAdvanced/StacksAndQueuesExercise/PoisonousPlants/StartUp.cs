using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var count = int.Parse(Console.ReadLine());
        var plants = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var days = new int[count];

        var proximityStack = new Stack<int>();
        proximityStack.Push(0);

        for (var i = 1; i < count; i++)
        {
            var maxDays = 0;

            while (proximityStack.Count > 0 &&
                   plants[proximityStack.Peek()] >= plants[i])
            {
                maxDays = Math.Max(maxDays, days[proximityStack.Pop()]);
            }

            if (proximityStack.Count > 0)
            {
                days[i] = maxDays + 1;
            }
            proximityStack.Push(i);
        }

        Console.WriteLine(days.Max());
    }
}