using System;

namespace p11_ConvertSpeedUnits
{
    class ConvertSpeedUnits
    {
        static void Main(string[] args)
        {
            float meters = float.Parse(Console.ReadLine());
            float hours = float.Parse(Console.ReadLine());
            float minutes = float.Parse(Console.ReadLine());
            float seconds = float.Parse(Console.ReadLine());

            float timeInHours = hours + minutes / 60.0f + seconds / 3600.0f;
            float kmPerHour = meters / 1000.0f / timeInHours;
            float metersPerSecond = kmPerHour / 3.6f;
            float milesPerHours = meters / 1609.0f / timeInHours;
            Console.WriteLine($"{metersPerSecond}\r\n" +
                $"{kmPerHour}\r\n" +
                $"{milesPerHours}");
        }
    }
}
