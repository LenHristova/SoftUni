namespace P10_CarSalesman
{
    using System;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main()
        {
            var engines = new Dictionary<string, Engine>();
            var cars = new List<Car>();

            AddNewEngines(engines);

            AddNewCars(engines, cars);

            Console.WriteLine(string.Join(Environment.NewLine, cars));
        }

        private static void AddNewCars(Dictionary<string, Engine> engines, List<Car> cars)
        {
            var carsCount = int.Parse(Console.ReadLine());
            for (int car = 0; car < carsCount; car++)
            {
                var carSpecifics = Console.ReadLine()?
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                if (carSpecifics == null || carSpecifics.Length < 2) continue;

                var model = carSpecifics[0];
                var engineModel = carSpecifics[1];
                var engine = engines[engineModel];

                var currentCar = new Car(model, engine);

                if (carSpecifics.Length > 2)
                {
                    var specification = carSpecifics[2];
                    AddCarSpecification(specification, currentCar);
                }

                if (carSpecifics.Length > 3)
                {
                    var specification = carSpecifics[3];
                    AddCarSpecification(specification, currentCar);
                }

                cars.Add(currentCar);
            }
        }

        private static void AddCarSpecification(string specification, Car car)
        {
            if (int.TryParse(specification, out int _))
            {
                car.Weight = specification;
            }
            else
            {
                car.Color = specification;
            }
        }

        private static void AddNewEngines(Dictionary<string, Engine> engines)
        {
            var engineCount = int.Parse(Console.ReadLine());
            for (int engine = 0; engine < engineCount; engine++)
            {
                var engineSpecifics = Console.ReadLine()?
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                if (engineSpecifics == null || engineSpecifics.Length < 2) continue;

                var model = engineSpecifics[0];
                var power = engineSpecifics[1];

                var currentEngine = new Engine(model, power);

                if (engineSpecifics.Length > 2)
                {
                    var specification = engineSpecifics[2];
                    AddEngineSpecification(specification, currentEngine);
                }
                if (engineSpecifics.Length > 3)
                {
                    var specification = engineSpecifics[3];
                    AddEngineSpecification(specification, currentEngine);
                }

                engines[model] = currentEngine;
            }
        }

        private static void AddEngineSpecification(string specification, Engine engine)
        {
            if (int.TryParse(specification, out int _))
            {
                engine.Displacement = specification;
            }
            else
            {
                engine.Efficiency = specification;
            }
        }
    }
}
