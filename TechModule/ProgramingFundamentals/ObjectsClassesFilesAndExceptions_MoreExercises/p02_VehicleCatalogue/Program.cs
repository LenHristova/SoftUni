using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace p02_VehicleCatalogue
{
    class Vehicle
    {
        public string Type { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Horsepower { get; set; }

        public override string ToString()
        {
            return $"Type: {Type}{Environment.NewLine}" +
                   $"Model: {Model}{Environment.NewLine}" +
                   $"Color: {Color}{Environment.NewLine}" +
                   $"Horsepower: {Horsepower}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> cataloge = new List<Vehicle>();

            FillCatalog(cataloge);

            LookUp(cataloge);

            double carAvarageHorsepower = GetAvarageHorsepower(cataloge, "Car");
            double truckAvarageHorsepower = GetAvarageHorsepower(cataloge, "Truck");

            Console.WriteLine($"Cars have average horsepower of: {carAvarageHorsepower:F2}.");
            Console.WriteLine($"Trucks have average horsepower of: {truckAvarageHorsepower:F2}.");
        }

        private static double GetAvarageHorsepower(List<Vehicle> cataloge, string vehicleType)
        {
            return cataloge
                .Where(vehicle => vehicle.Type == vehicleType)
                .Select(vehicle => vehicle.Horsepower)
                .DefaultIfEmpty()
                .Average();
        }

        private static void LookUp(List<Vehicle> catalogeCars)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Close the Catalogue")
                    break;

                string model = input;
                Console.WriteLine(catalogeCars.Find(vehicle => vehicle.Model == model));
            }
        }

        private static void FillCatalog(List<Vehicle> catalogeCars)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                    break;

                string[] vehicleInfo = input
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                catalogeCars.Add(new Vehicle
                {
                    Type = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(vehicleInfo[0]),
                    Model = vehicleInfo[1],
                    Color = vehicleInfo[2],
                    Horsepower = int.Parse(vehicleInfo[3])
                });
            }
        }
    }
}
