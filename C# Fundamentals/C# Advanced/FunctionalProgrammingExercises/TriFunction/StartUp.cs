using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        int num = int.Parse(Console.ReadLine());
        var names = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        Func<string, bool> func = str => str.Sum(ch => ch) >= num;

        Console.WriteLine(names.FirstOrDefault(func));
    }

}