using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var snowmen = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        
        while (snowmen.Count > 1)
        {
            var losesIndeces = new HashSet<int>();

            for (int i = 0; i < snowmen.Count; i++)
            {
                if (snowmen.Count - losesIndeces.Count == 1)
                {
                    return;
                }

                if (losesIndeces.Contains(i))
                {
                    continue;
                }

                var attacker = i;
                var target = snowmen[i];

                if (target >= snowmen.Count)
                {
                    target %= snowmen.Count;
                }

                var diff = Math.Abs(attacker - target);

                if (attacker == target)
                {
                    Console.WriteLine($"{attacker} performed harakiri");
                    losesIndeces.Add(attacker);
                }
                else if (diff % 2 == 0)
                {
                    Console.WriteLine($"{attacker} x {target} -> {attacker} wins");
                    losesIndeces.Add(target);
                }
                else if (diff % 2 != 0)
                {
                    Console.WriteLine($"{attacker} x {target} -> {target} wins");

                    losesIndeces.Add(attacker);
                }
            }

            foreach (var index in losesIndeces.OrderByDescending(x => x))
            {
                if (index >= 0 && index < snowmen.Count)
                {
                    snowmen.RemoveAt(index);
                }               
            }
        }
    }
}