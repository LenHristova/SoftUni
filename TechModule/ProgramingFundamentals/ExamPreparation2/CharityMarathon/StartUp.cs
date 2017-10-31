using System;

namespace CharityMarathon
{
    class StartUp
    {
        static void Main()
        {
            int days = int.Parse(Console.ReadLine());
            int runners = int.Parse(Console.ReadLine());
            int laps = int.Parse(Console.ReadLine());
            int trackLength = int.Parse(Console.ReadLine());
            int trackCapacity = int.Parse(Console.ReadLine());
            decimal money = decimal.Parse(Console.ReadLine());

            trackCapacity *= days;
            runners = Math.Min(runners, trackCapacity);

            long totalMeters = (long)runners * laps * trackLength;
            decimal totalKm = (decimal)totalMeters / 1000;

            decimal allMoney = money * totalKm;

            Console.WriteLine($"Money raised: {allMoney:F2}");
        }
    }
}