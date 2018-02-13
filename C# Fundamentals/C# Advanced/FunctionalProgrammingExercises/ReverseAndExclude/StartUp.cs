using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);

        var divisor = int.Parse(Console.ReadLine());

        numbers = numbers.Where(IsNotDivisible(divisor)).Reverse();

        Console.WriteLine(string.Join(" ", numbers));
    }

    static Func<int, bool> IsNotDivisible(int div)
    {
        return n => n % div != 0;
    }
}