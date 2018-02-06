using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Action<string> print = s => Console.WriteLine(s);
        Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList()
            .ForEach(print);
    }
}