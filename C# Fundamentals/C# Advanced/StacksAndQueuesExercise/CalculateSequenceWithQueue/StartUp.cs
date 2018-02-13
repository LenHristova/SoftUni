using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

class StartUp
{
    //We are given the following sequence of numbers:
    //•	S1 = N
    //•	S2 = S1 + 1
    //•	S3 = 2*S1 + 1
    //•	S4 = S1 + 2
    //•	S5 = S2 + 1
    //•	S6 = 2*S2 + 1
    //•	S7 = S2 + 2
    //•	S8 = S3 + 1
    //•	…
    //Using the Queue<T> class, write a program to print its first 50 members for given N.

    static void Main()
    {
        var n = decimal.Parse(Console.ReadLine());

        var queue = new Queue<decimal>();
        Console.Write(n + " ");
        queue.Enqueue(n);

        var membersCount = 1;

        while (true)
        {
            var currentNum = queue.Dequeue();
            var nextThreeMembers = new []
                {
                    currentNum + 1,
                    2 * currentNum + 1,
                    currentNum + 2
                };

            foreach (var member in nextThreeMembers)
            {
                Console.Write(member + " ");
                if (++membersCount == 50)
                {
                    Console.WriteLine();
                    return;
                }
                queue.Enqueue(member);
            }
        }
    }
}