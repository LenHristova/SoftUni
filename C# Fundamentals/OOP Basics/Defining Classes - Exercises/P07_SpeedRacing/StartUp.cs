namespace P07_SpeedRacing
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main()
        {
            var cars = new HashSet<Car>();

            GetCars(cars);

            MoveCars(cars);

            Console.WriteLine(string.Join(Environment.NewLine, cars));
        }

        private static void MoveCars(HashSet<Car> cars)
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                if (input == null) continue;

                var commandsArgs = input.Split();
                var model = commandsArgs[1];
                var amountOfKm = int.Parse(commandsArgs[2]);
                cars.FirstOrDefault(c => c.Equals(new Car(model)))?.Drive(amountOfKm);
            }
        }

        private static void GetCars(HashSet<Car> cars)
        {
            var carsCount = int.Parse(Console.ReadLine());
            for (int car = 0; car < carsCount; car++)
            {
                var carInfo = Console.ReadLine()?.Split();

                if (carInfo == null) continue;

                var model = carInfo[0];
                var fuelAmount = double.Parse(carInfo[1]);
                var fuelConsumptionFor1Km = double.Parse(carInfo[2]);

                cars.Add(new Car(model, fuelAmount, fuelConsumptionFor1Km));
            }
        }
    }
}