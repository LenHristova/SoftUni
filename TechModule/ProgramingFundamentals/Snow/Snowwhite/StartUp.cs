using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var dwarfs = new HashSet<Dwarf>();

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "Once upon a time")
                break;

            var dwarfData = input
                .Split(new[] {' ', '\t', '\n', '<', ':', '>'}, StringSplitOptions.RemoveEmptyEntries);

            var dwarfName = dwarfData[0];
            var dwarfHatColor = dwarfData[1];
            var dwarfPhysics = int.Parse(dwarfData[2]);

            var currentDwarf = new Dwarf(dwarfName, dwarfHatColor, dwarfPhysics);

            if (dwarfs.Contains(currentDwarf) && 
                dwarfs.First(d => d.Equals(currentDwarf)).Physics < currentDwarf.Physics)
            {
                dwarfs.Remove(currentDwarf);
            }

            dwarfs.Add(currentDwarf);
        }

        var hatColorCount = dwarfs
            .GroupBy(d => d.HatColor)
            .ToDictionary(d => d.Key, d => d.ToList().Count);

        foreach (var dwarf in dwarfs
            .OrderByDescending(d => d.Physics)
            .ThenByDescending(d => hatColorCount[d.HatColor]))
        {
            Console.WriteLine(dwarf);
        }
    }
}