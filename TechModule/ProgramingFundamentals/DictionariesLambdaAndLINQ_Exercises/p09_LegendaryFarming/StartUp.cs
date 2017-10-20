using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        Dictionary<string, string> legendaryItems = new Dictionary<string, string>
        {
            {"shards", "Shadowmourne" },
            {"fragments", "Valanyr" },
            { "motes", "Dragonwrath"}
        };
        SortedDictionary<string, int> materialsQuantity = new SortedDictionary<string, int>
        {
            {"shards", 0 },
            {"fragments", 0 },
            { "motes", 0}
        };
        SortedDictionary<string, int> junksQuantity = new SortedDictionary<string, int>();

        bool hasLegendaryItem = false;
        while (!hasLegendaryItem)
        {
            string[] tokens = Console.ReadLine()
                .ToLower()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < tokens.Length; i+=2)
            {
                int quantity = int.Parse(tokens[i]);
                string material = tokens[i + 1];

                if (materialsQuantity.ContainsKey(material))
                {
                    materialsQuantity[material] += quantity;
                    if (materialsQuantity[material] >= 250)
                    {
                        Console.WriteLine($"{legendaryItems[material]} obtained!");
                        materialsQuantity[material] -= 250;
                        hasLegendaryItem = true;
                        break;
                    }
                }
                else
                {
                    if (!junksQuantity.ContainsKey(material))
                    {
                        junksQuantity[material] = 0;
                    }
                    junksQuantity[material] += quantity;
                }
            }
        }

        foreach (var materialQuantity in materialsQuantity.OrderByDescending(m => m.Value))
        {
            Console.WriteLine($"{materialQuantity.Key}: {materialQuantity.Value}");
        }
        foreach (var junkQuantity in junksQuantity)
        {
            Console.WriteLine($"{junkQuantity.Key}: {junkQuantity.Value}");
        }
    }
}