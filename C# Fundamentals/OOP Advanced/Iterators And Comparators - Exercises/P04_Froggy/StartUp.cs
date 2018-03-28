using System;
using System.Linq;

namespace P04_Froggy
{
    public class StartUp
    {
        static void Main()
        {
            var stonesNumbers = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse);

            var lake = new Lake<int>(stonesNumbers);

            Console.WriteLine(string.Join(", ", lake));
        }
    }
}