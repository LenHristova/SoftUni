using System;
using System.Linq;
using System.Numerics;

namespace ConvertFromBaseNToBase10
{
    class StartUp
    {
        static void Main()
        {
             string[] parameters = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int n = int.Parse(parameters[0]);
            int[] baseN = parameters[1]
                .Reverse()
                .Select(ch => int.Parse(ch.ToString()))
                .ToArray();

            BigInteger base10 = 0;

            for (int i = 0; i < baseN.Length; i++)
            {
                base10 += BigInteger.Pow(n, i) * baseN[i];
            }

            Console.WriteLine(base10);
        }
    }
}