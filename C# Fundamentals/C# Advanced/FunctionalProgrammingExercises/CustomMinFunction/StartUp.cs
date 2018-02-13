using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
 
        var num = numbers.First(Smallest(numbers));
        Console.WriteLine(num);
    }

    static Func<int, bool> Smallest(int[] arr)
    {
        var min = int.MaxValue;
        foreach (var currentNum in arr)
        {
            if (currentNum < min)
            {
                min = currentNum;
            }
        }

        return num => num == min;
    }
}