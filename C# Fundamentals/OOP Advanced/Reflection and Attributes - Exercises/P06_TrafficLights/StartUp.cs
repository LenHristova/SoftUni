using System;
using System.Linq;

public class StartUp
{
    static void Main()
    {
        var traficLights = Console.ReadLine()
            .Split()
            .Select(light => new TrafficLight(light))
            .ToList();

        var lightChangeCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < lightChangeCount; i++)
        {
            foreach (var trafficLight in traficLights)
            {
                trafficLight.NextLight();
                Console.Write(trafficLight.CurrentLight + " ");
            }

            Console.WriteLine();
        }
    }
}