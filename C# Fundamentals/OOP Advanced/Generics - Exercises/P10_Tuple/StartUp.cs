using System;

public class StartUp
{
    static void Main()
    {
        var personArgs = Console.ReadLine().Split();
        var name = personArgs[0] + " " + personArgs[1];
        var adsress = personArgs[2];
        var person = new Tuple<string, string>(name, adsress);
        Console.WriteLine(person);

        var person2Args = Console.ReadLine().Split();
        var name2 = person2Args[0];
        var beerAmount = int.Parse(person2Args[1]);
        var person2 = new Tuple<string, int>(name2, beerAmount);
        Console.WriteLine(person2);

        var args = Console.ReadLine().Split();
        var intArg = int.Parse(args[0]);
        var doubleArg = double.Parse(args[1]);
        var tuple = new Tuple<int, double>(intArg, doubleArg);
        Console.WriteLine(tuple);
    }
}