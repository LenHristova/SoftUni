using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p04_GrabAndGo
{
    class StartUp
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine()
                .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int searchedNum = int.Parse(Console.ReadLine());
            numbers = numbers.Reverse().ToArray();
            numbers = numbers.SkipWhile(n => n == searchedNum)
                .Take().ToArray();
            int sumRange = numbers.Sum();
            Console.WriteLine(sumRange);
        }
    }
}
