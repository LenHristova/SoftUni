using System;

public class StartUp
{
    static void Main()
    {
        var personArgs = Console.ReadLine().Split();
        var name = personArgs[0] + " " + personArgs[1];
        var adsress = personArgs[2];
        var town = personArgs[3];
        var person = new Threeuple<string, string, string>(name, adsress, town);
        Console.WriteLine(person);

        var person2Args = Console.ReadLine().Split();
        var name2 = person2Args[0];
        var beerAmount = int.Parse(person2Args[1]);
        var isDrunk = person2Args[2] == "drunk";
        var person2 = new Threeuple<string, int, bool>(name2, beerAmount, isDrunk);
        Console.WriteLine(person2);

        var person3Args = Console.ReadLine().Split();
        var name3 = person3Args[0];
        var accountBalance = double.Parse(person3Args[1]);
        var bankName = person3Args[2];
        var person3 = new Threeuple<string, double, string>(name3, accountBalance, bankName);
        Console.WriteLine(person3);
    }
}
