using System;
using System.Linq;

namespace ReverseString
{
    class StartUp
    {
        static void Main()
        {
            var reversedInput = new string(Console.ReadLine().Reverse().ToArray());
            Console.WriteLine(reversedInput);
        }
    }
}