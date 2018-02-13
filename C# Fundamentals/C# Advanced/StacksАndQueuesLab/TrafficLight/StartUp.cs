using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var carsCount = int.Parse(Console.ReadLine());

        var queue = new Queue<string>();

        var carsCounter = 0;
        while (true)
        {
            var input = Console.ReadLine();

            if (input == "end")
            {
                break;
            }

            if (input == "green")
            {
                for (var car = 0; car < carsCount; car++)
                {
                    if (queue.Count == 0)
                    {
                        break;
                    }
                    Console.WriteLine($"{queue.Dequeue()} passed!");
                    carsCounter++;
                }

                continue;
            }

            queue.Enqueue(input);
        }

        Console.WriteLine($"{carsCounter} cars passed the crossroads.");
    }
}