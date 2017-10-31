using System;

namespace п01_SinoTheWalker
{
    class StartUp
    {
        static void Main()
        {
            DateTime time = DateTime.ParseExact(
                Console.ReadLine(), "HH:mm:ss", null);

            double steps = double.Parse(Console.ReadLine());
            double seconds = double.Parse(Console.ReadLine());
            double neededTime = (double)(((long)steps * seconds) % 86400);

            string result = time.AddSeconds(neededTime).ToString("HH:mm:ss");

            Console.WriteLine($"Time Arrival: {result}");
        }
    }
}