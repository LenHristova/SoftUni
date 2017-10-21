using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p03_ImmuneSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int immuneSystemFullStrength = int.Parse(Console.ReadLine());
            int initualHealth = immuneSystemFullStrength;
            List<string> defeatedViruses = new List<string>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                    break;

                string virusName = input;
                int virusStrength = virusName.ToCharArray()
                    .Select(ch => (int)ch)
                    .Sum() / 3;

                int timeToDefeat = virusStrength * virusName.Length;
                if (!defeatedViruses.Contains(virusName))
                {
                    defeatedViruses.Add(virusName);
                }
                else
                {
                    timeToDefeat /= 3;
                }

                Console.WriteLine($"Virus {virusName}: " +
                                  $"{virusStrength} => {timeToDefeat} seconds");

                if (initualHealth <= timeToDefeat)
                {
                    Console.WriteLine("Immune System Defeated.");
                    return;
                }

                Console.WriteLine($"{virusName} defeated in " +
                                  $"{timeToDefeat / 60}m {timeToDefeat % 60}s.");

                initualHealth -= timeToDefeat;
                Console.WriteLine($"Remaining health: {initualHealth}");

                initualHealth += Math.Min(
                    (int)(initualHealth * 0.2),
                    immuneSystemFullStrength - initualHealth);
            }

            Console.WriteLine($"Final Health: {initualHealth}");
        }
    }
}