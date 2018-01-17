using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        var queue = new Queue<int>();
        queue.Enqueue(n);

        var counter = 0;

        while (true)
        {
            var s1 = queue.Dequeue();
            Console.Write(s1);

            counter++;
            if (counter == 50)
            {
                break;
            }

            Console.Write(", ");

            queue.Enqueue(s1 + 1);
            queue.Enqueue(2 * s1 + 1);
            queue.Enqueue(s1 + 2);
        }

        Console.WriteLine();
    }
}