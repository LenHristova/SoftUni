using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    class Dragon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; } 
        public int Armor { get; set; }

        public static void PrintDragonStats(Dragon dragon)
        { 
            Console.WriteLine($"-{dragon.Name} -> " +
                              $"damage: {dragon.Damage}, " +
                              $"health: {dragon.Health}, " +
                              $"armor: {dragon.Armor}");
        }
    }

    class DragonType
    {
        public string Type { get; set; }
        public List<Dragon> Dragons { get; set; }

        private double AvarageDamage => Dragons
            .Select(d => d.Damage)
            .Average();

        private double AvarageHealth => Dragons
            .Select(d => d.Health)
            .Average();

        private double AvarageArmor => Dragons
            .Select(d => d.Armor)
            .Average();

        public void PrintDragonTypeStats(DragonType dt)
        {          
            Console.WriteLine($"{dt.Type}::(" +
                              $"{dt.AvarageDamage:F2}/" +
                              $"{dt.AvarageHealth:F2}/" +
                              $"{dt.AvarageArmor:F2})");
            dt.Dragons.OrderBy(d => d.Name).ToList().ForEach(dr => Dragon.PrintDragonStats(dr));
        }
    }
    static void Main()
    {
        List<DragonType> dragonsStats = new List<DragonType>();

        GetDragons(dragonsStats);
        dragonsStats.ForEach(d => d.PrintDragonTypeStats(d));
    }

    static void GetDragons(List<DragonType> dragonsStats)
    {
        int dragonsCount = int.Parse(Console.ReadLine());
        for (int currDragon = 0; currDragon < dragonsCount; currDragon++)
        {
            string[] dragonParam = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string type = dragonParam[0];
            if (dragonsStats.All(dragonType => dragonType.Type != type))
            {
                dragonsStats.Add(new DragonType
                {
                    Type = type,
                    Dragons = new List<Dragon>()
                });
            }

            string dragonName = dragonParam[1];
            if (dragonsStats.Find(dragonType => dragonType.Type == type)
                .Dragons.All(dragon => dragon.Name != dragonName))
            {
                dragonsStats.Find(dragonType => dragonType.Type == type).Dragons.Add(new Dragon
                {
                    Name = dragonName
                });
            }
            Dragon currentDragon = dragonsStats.Find(dragonType => dragonType.Type == type)
                .Dragons.Find(dragon => dragon.Name == dragonName);

            currentDragon.Damage = int.TryParse(dragonParam[2], out int d) ? d : 45;
            currentDragon.Health = int.TryParse(dragonParam[3], out int h) ? h : 250;
            currentDragon.Armor = int.TryParse(dragonParam[4], out int a) ? a : 10;
        }
    }
}