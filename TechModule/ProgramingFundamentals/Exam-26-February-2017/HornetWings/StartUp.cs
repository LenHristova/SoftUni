using System;

namespace HornetWings
{
    class StartUp
    {
        static void Main()
        {
            int wingFlaps = int.Parse(Console.ReadLine());
            double distancePer1000Flaps = double.Parse(Console.ReadLine());
            int endurance = int.Parse(Console.ReadLine());

            decimal distancePer1Flap = (decimal)distancePer1000Flaps / 1000;
            decimal distance = distancePer1Flap * wingFlaps;
            Console.WriteLine($"{distance:F2} m.");

            decimal seconds = (decimal)wingFlaps / 100;
            seconds += wingFlaps / endurance * 5;
            Console.WriteLine($"{seconds} s.");
        }
    }
}