using System;
using System.Collections.Generic;
using System.Linq;

namespace P02_CarsSalesman
{
    public class EnginesCatalog
    {
        private static List<Engine> engines = new List<Engine>();

        private void AddEngine(string engineSpecifications)
        {
            string[] parameters = engineSpecifications.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string model = parameters[0];
            int power = int.Parse(parameters[1]);
            int displacement;

            if (parameters.Length == 3 && int.TryParse(parameters[2], out displacement))
            {
                engines.Add(new Engine(model, power, displacement));
            }
            else if (parameters.Length == 3)
            {
                string efficiency = parameters[2];
                engines.Add(new Engine(model, power, efficiency));
            }
            else if (parameters.Length == 4)
            {
                displacement = int.Parse(parameters[2]);
                string efficiency = parameters[3];
                engines.Add(new Engine(model, power, displacement, efficiency));
            }
            else
            {
                engines.Add(new Engine(model, power));
            }
        }

        public static Engine FirstOrDefaultEngine(string model)
        {
            return engines.FirstOrDefault(e => e.model == model);
        }

        public static void LoadCatalog()
        {
            EnginesCatalog enginesCatalog = new EnginesCatalog();
            //Console.Write("Enter engine count: ");
            int enginesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i <= enginesCount; i++)
            {
                //Console.WriteLine($"Enter engine specifications N:{i}:");
                enginesCatalog.AddEngine(Console.ReadLine());
            }
        }
    }
}