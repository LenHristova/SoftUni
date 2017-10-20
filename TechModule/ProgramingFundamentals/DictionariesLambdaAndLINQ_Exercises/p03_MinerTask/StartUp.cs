using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        Dictionary<string, long> resourceQuantity = new Dictionary<string, long>();
        string input = Console.ReadLine();
        while (input != "stop")
        {
            string resource = input;
            int quantity = int.Parse(Console.ReadLine());

            if (!resourceQuantity.ContainsKey(resource))
            {
                resourceQuantity[resource] = 0L;
            }
            resourceQuantity[resource] += quantity;

            input = Console.ReadLine();
        }

        foreach (var resource in resourceQuantity)
        {
            Console.WriteLine($"{resource.Key} -> {resource.Value}");
        }     
    }
}

