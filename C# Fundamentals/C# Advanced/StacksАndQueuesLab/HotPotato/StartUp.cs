using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var kidsNames = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var hotPotato = int.Parse(Console.ReadLine());

        var queue = new Queue<string>(kidsNames);

        while (queue.Count > 1)
        {
            for (var toss = 1; toss < hotPotato; toss++)
            {
                queue.Enqueue(queue.Dequeue());
            }

            Console.WriteLine($"Removed {queue.Dequeue()}");
        }

        Console.WriteLine($"Last is {queue.Dequeue()}");
    }
}