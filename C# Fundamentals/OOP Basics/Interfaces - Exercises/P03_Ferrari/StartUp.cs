using System;
using P03_Ferrari.Models;

namespace P03_Ferrari
{
    class StartUp
    {
        static void Main()
        {
            var driverName = Console.ReadLine();
            ICar ferrari = new Car("488-Spider", driverName);
            Console.WriteLine(ferrari);
        }
    }
}