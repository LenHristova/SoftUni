using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Dictionary<string, SortedDictionary<string, int[]>> dragonsStats = new Dictionary<string, SortedDictionary<string, int[]>>();

        int dragonsCount = int.Parse(Console.ReadLine());
        for (int currDragon = 0; currDragon < dragonsCount; currDragon++)
        {
            string[] dragonParam = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string type = dragonParam[0];
            string name = dragonParam[1];
            int damage = int.TryParse(dragonParam[2], out int d) ? d : 45;
            int health = int.TryParse(dragonParam[3], out int h) ? h : 250;
            int armor = int.TryParse(dragonParam[4], out int a) ? a : 10;

            if (!dragonsStats.ContainsKey(type))
            {
                dragonsStats[type] = new SortedDictionary<string, int[]>();
            }
            if (!dragonsStats[type].ContainsKey(name))
            {
                dragonsStats[type][name] = new int[3];
            }
            dragonsStats[type][name][0] = damage;
            dragonsStats[type][name][1] = health;
            dragonsStats[type][name][2] = armor;
        }

        foreach (var type in dragonsStats)
        {
            double damageAverage = type.Value.Values
                .Average(x => x[0]);
            double healthAverage = type.Value.Values
                .Average(x => x[1]);
            double armorAverage = type.Value.Values
                .Average(x => x[2]);

            Console.WriteLine($"{type.Key}::" +
                              $"({damageAverage:F2}/" +
                              $"{healthAverage:F2}/" +
                              $"{armorAverage:F2})");
            foreach (var dragon in type.Value)
            {
                Console.WriteLine($"-{dragon.Key} -> " +
                                  $"damage: {dragon.Value[0]}, " +
                                  $"health: {dragon.Value[1]}, " +
                                  $"armor: {dragon.Value[2]}");
            }
        }
    }
}