namespace P08_RawData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var fragiles = new List<Car>();
            var flamables = new List<Car>();

            //Add owned cars in lists by their cargo type
            GetAndClassifyCars(fragiles, flamables);

            var wantedCargoType = Console.ReadLine();

            if (wantedCargoType?.ToLower() == "fragile")
            {
                Console.WriteLine(string.Join(Environment.NewLine, fragiles
                    .Where(c => !c.HasTiresPressure())));
            }
            else if (wantedCargoType?.ToLower() == "flamable")
            {
                Console.WriteLine(string.Join(Environment.NewLine, flamables
                    .Where(c => c.HasEnginePower())));
            }
        }

        private static void GetAndClassifyCars(List<Car> fragiles, List<Car> flamables)
        {
            var carsCount = int.Parse(Console.ReadLine());
            for (int car = 0; car < carsCount; car++)
            {
                var carInfo = Console.ReadLine()?.Split();
                if (carInfo == null) continue;

                var model = carInfo[0];

                var engineSpeed = int.Parse(carInfo[1]);
                var enginePower = int.Parse(carInfo[2]);
                var engine = new Engine(engineSpeed, enginePower);

                var cargoWeight = int.Parse(carInfo[3]);
                var cargoType = carInfo[4];
                var cargo = new Cargo(cargoWeight, cargoType);

                Tire[] tires = GetTires(carInfo);

                var currentCar = new Car(model, engine, cargo, tires);

                if (cargoType.ToLower() == "fragile")
                {
                    fragiles.Add(currentCar);
                }
                else if (cargoType.ToLower() == "flamable")
                {
                    flamables.Add(currentCar);
                }
            }
        }

        //Get tires. Tires' count must be 4
        //If input contains less then 4 tires, the tires pressure and age are marks as 0
        //If input contains more then 4 tires, the useless tires are ignored
        private static Tire[] GetTires(string[] carInfo)
        {
            var tires = new List<Tire>();
            for (int i = 0; i < 8; i += 2)
            {
                var tirePressure = i + 5 < carInfo.Length ? double.Parse(carInfo[i + 5]) : 0;
                var tireAge = i + 6 < carInfo.Length ? int.Parse(carInfo[i + 6]) : 0;
                tires.Add(new Tire(tirePressure, tireAge));
            }

            return tires.ToArray();
        }
    }
}