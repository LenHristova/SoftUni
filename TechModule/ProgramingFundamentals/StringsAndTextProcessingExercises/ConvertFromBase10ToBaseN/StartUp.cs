using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ConvertFromBase10ToBaseN
{
    class StartUp
    {
        static void Main()
        {
            BigInteger[] parameters = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(BigInteger.Parse)
                .ToArray();

            byte n = (byte)parameters[0];
            BigInteger base10 = parameters[1];

            StringBuilder baseN = new StringBuilder();
            while (base10 > 0)
            {
                baseN.Insert(0, base10 % n);
                base10 /= n;
            }

            Console.WriteLine(baseN);
        }
    }
}