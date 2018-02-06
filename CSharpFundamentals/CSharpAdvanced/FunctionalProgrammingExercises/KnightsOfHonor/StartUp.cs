using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Action<string> print = s => Console.WriteLine(s);
        Func<string, string> addSir = s => "Sir" + " " + s; 

        Console.ReadLine()
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(addSir)
                    .ToList()
                    .ForEach(print);
    }
}