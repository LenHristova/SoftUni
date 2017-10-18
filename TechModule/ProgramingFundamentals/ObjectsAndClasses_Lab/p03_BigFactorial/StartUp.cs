using System;
using System.Numerics;

class StartUp
{
    static void Main()
    {
        int num = int.Parse(Console.ReadLine());

        BigInteger factorial = 1;

        for (int i = 1; i <= num; i++)
        {
            factorial *= i;
        }
        Console.WriteLine(factorial);
    }
}