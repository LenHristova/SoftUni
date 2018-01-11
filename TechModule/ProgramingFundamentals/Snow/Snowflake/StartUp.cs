using System;
using System.Text.RegularExpressions;

class StartUp
{
    static void Main()
    {
        var surface = Console.ReadLine();
        var mantle = Console.ReadLine();
        var middle = Console.ReadLine();
        var mantle2 = Console.ReadLine();
        var surface2 = Console.ReadLine();

        var surfacePattern = "^[^A-Za-z0-9]+$";
        var mantlePattern = "^[0-9_]+$";
        var corePattern = "([A-Za-z])+";
        var middlePattern = "^[^A-Za-z0-9]+[0-9_]+[A-Za-z]+[0-9_]+[^A-Za-z0-9]+";

        if (Regex.IsMatch(surface, surfacePattern)
        && Regex.IsMatch(mantle, mantlePattern)
        && Regex.IsMatch(middle, middlePattern)
        && Regex.IsMatch(surface2, surfacePattern)
        && Regex.IsMatch(mantle2, mantlePattern))
        {
            Console.WriteLine("Valid");
            var core = Regex.Match(middle, corePattern);
            Console.WriteLine(core.Length);
        }
        else
        {
            Console.WriteLine("Invalid");
        }
    }
}