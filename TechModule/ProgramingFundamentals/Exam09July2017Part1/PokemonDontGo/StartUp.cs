using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonDontGo
{
    class StartUp
    {
        static void Main()
        {
            List<int> distances = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            long sum = 0L;
            while (distances.Count > 0)
            {
                int index = int.Parse(Console.ReadLine());

                int num;
                if (index < 0)
                {
                    num = distances.First();
                    distances.RemoveAt(0);
                    distances.Insert(0, distances.Last());
                }
                else if (index > distances.Count - 1)
                {
                    num = distances.Last();
                    distances.RemoveAt(distances.Count - 1);
                    distances.Add(distances.First());
                }
                else
                {
                    num = distances[index];
                    distances.RemoveAt(index);
                }

                sum += num;

                distances = distances
                    .Select(n => n <= num ? n + num : n - num)
                    .ToList();
            }

            Console.WriteLine(sum);
        }
    }
}