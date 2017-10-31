using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceRally
{
    class Driver
    {
        public string Name { get; set; }
        public double Fuel { get; set; }
        public int ReachedZone { get; set; }
        public bool IsOutOfRace => Fuel <= 0;
    }
    class StartUp
    {
        static void Main()
        {
            string[] drivers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            double[] zonesFuel = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            int[] checkpointIndexes = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            List<Driver> driversPerformance = drivers
                .Select(d => new Driver {Name = d, Fuel = d[0], ReachedZone = 0})
                .ToList();

            Drive(driversPerformance, zonesFuel, checkpointIndexes);

            foreach (var driver in driversPerformance)
            {
                Console.WriteLine(driver.IsOutOfRace
                    ? $"{driver.Name} - reached {driver.ReachedZone}"
                    : $"{driver.Name} - fuel left {driver.Fuel:F2}");
            }
        }

        private static void Drive(List<Driver> driversFuel, double[] zonesFuel, int[] checkpointIndexes)
        {
            foreach (var driver in driversFuel)
            {
                for (int zoneIndex = 0; zoneIndex < zonesFuel.Length; zoneIndex++)
                {
                    if (driver.IsOutOfRace)
                    {
                        continue;
                    }

                    driver.ReachedZone = zoneIndex;

                    if (checkpointIndexes.Contains(zoneIndex))
                    {
                        driver.Fuel += zonesFuel[zoneIndex];
                    }
                    else
                    {
                        driver.Fuel -= zonesFuel[zoneIndex];
                    }
                }
            }
        }
    }
}