using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var endRange = int.Parse(Console.ReadLine());
        var dividers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var numbers = Enumerable.Range(1, endRange < 0 ? 1 : endRange)
            .Where(IsDivisibleBy(dividers));

        Console.WriteLine(string.Join(" ", numbers));
    }

    static Func<int, bool> IsDivisibleBy(int[] arr)
    {
        return num =>
        {
            foreach (var i in arr)
            {
                if (num % i != 0)
                {
                    return false;
                }
            }
            return true;
        };
    }
}