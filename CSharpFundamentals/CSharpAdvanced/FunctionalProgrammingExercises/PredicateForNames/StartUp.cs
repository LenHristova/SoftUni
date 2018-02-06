using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var neededLength = int.Parse(Console.ReadLine());

        var names = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

        foreach (var s in names.Where(FilterByLength(neededLength)))
        {
            Console.WriteLine(s);
        }
    }

    static Func<string, bool> FilterByLength(int length)
    {
        return s => s.Length <= length;
    }
}