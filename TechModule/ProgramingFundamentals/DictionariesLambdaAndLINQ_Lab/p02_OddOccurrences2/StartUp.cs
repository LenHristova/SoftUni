using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var counts = Console.ReadLine().ToLower()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(w => w)
            .ToDictionary(d => d.Key, d => d.Count());
        
        Console.WriteLine(string.Join(", ",
            counts.Where(c => c.Value % 2 != 0).Select(d => d.Key)));
    }
}