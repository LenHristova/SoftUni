using System;
using System.Collections.Generic;

class StartUp
{
    private static readonly Dictionary<int, long> Cache = new Dictionary<int, long>
    {
        { 0, 0 },
        { 1, 1 }
    };

    static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        Console.WriteLine(Fibonacci(n));
    }

    private static long Fibonacci(int n)
    {
        if (Cache.ContainsKey(n))
        {
            return Cache[n];
        }

        var result = Fibonacci(n - 2) + Fibonacci(n - 1);
        Cache.Add(n, result);
        return result;
    }
}