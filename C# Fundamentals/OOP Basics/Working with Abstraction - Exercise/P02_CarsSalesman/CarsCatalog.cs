using System;
using System.Collections.Generic;

namespace P02_CarsSalesman
{
    public class CarsCatalog
    {
        private static List<Car> Cars = new List<Car>();

        private void AddCar(string carSpecifications)
        {
            string[] parameters = carSpecifications.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string model = parameters[0];
            string engineModel = parameters[1];
            Engine engine = EnginesCatalog.FirstOrDefaultEngine(engineModel);
            int weight;

            if (parameters.Length == 3 && int.TryParse(parameters[2], out weight))
            {
                Cars.Add(new Car(model, engine, weight));
            }
            else if (parameters.Length == 3)
            {
                string color = parameters[2];
                Cars.Add(new Car(model, engine, color));
            }
            else if (parameters.Length == 4)
            {
                string color = parameters[3];
                weight = int.Parse(parameters[2]);
                Cars.Add(new Car(model, engine, weight, color));
            }
            else
            {
                Cars.Add(new Car(model, engine));
            }
        }

        public static void LoadCatalog()
        {
            CarsCatalog carsCatalog = new CarsCatalog();
            //Console.Write("Enter cars count: ");
            int carsCount = int.Parse(Console.ReadLine());
            for (int i = 1; i <= carsCount; i++)
            {
                //Console.WriteLine($"Enter car specifications N:{i}:");
                carsCatalog.AddCar(Console.ReadLine());
            }
        }

        public static void DisplayCars()
        {
            Console.WriteLine(string.Join(Environment.NewLine, Cars));
        }
    }
}

