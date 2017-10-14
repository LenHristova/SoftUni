using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        string[] words = Console.ReadLine().ToLower()
            .Split(new[] { ' ', '.', ',', ':', ';', '(', '"', '\'', ')', '[', ']', '\\', '/', '!', '?' },
                StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Length < 5)
            .OrderBy(w => w)
            .Distinct()
            .ToArray();
        Console.WriteLine(string.Join(", ", words));
    }
}