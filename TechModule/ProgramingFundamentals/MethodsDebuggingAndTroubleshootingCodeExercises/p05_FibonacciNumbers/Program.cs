using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p05_FibonacciNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            decimal fib = Fib(number);
            Console.WriteLine(fib);
        }

        private static decimal Fib(int number)
        {
            decimal oldFib = 0m;
            decimal newFib = 1m;

            for (int i = 0; i < number; i++)
            {
                decimal oldValueOfNewFib = newFib;
                newFib += oldFib;
                oldFib = oldValueOfNewFib;
            }
            return newFib;
        }
    }
}
