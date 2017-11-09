using System;
using System.Collections.Generic;
using System.Linq;

namespace HornetAssault
{
    class StartUp
    {
        static void Main()
        {
            List<long> beehives = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();

            List<long> hornets = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();

            long hornetsPower = hornets.Sum();

            for (int i = 0; i < beehives.Count; i++)
            {
                if (beehives[i] >= hornetsPower)
                {
                    beehives[i] -= hornetsPower;
                    hornets.RemoveAt(0);
                    if (!hornets.Any())
                    {
                        break;
                    }
                    hornetsPower = hornets.Sum();
                }
                else
                {
                    beehives[i] -= hornetsPower;
                }
            }

            beehives = beehives
                .Where(b => b > 0)
                .ToList();

            Console.WriteLine(beehives.Any()
                ? string.Join(" ", beehives)
                : string.Join(" ", hornets));
        }
    }
}