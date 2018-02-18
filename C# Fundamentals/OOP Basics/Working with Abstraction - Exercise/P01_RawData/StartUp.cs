using System;

namespace P01_RawData
{
    class StartUp
    {
        static void Main()
        {
            var rawData = new RawData();

            int lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                string carInfo = Console.ReadLine();
                rawData.AddCar(carInfo);
            }

            string cargoType = Console.ReadLine();
            Console.WriteLine(rawData.GetCarsByCargoType(cargoType));
        }
    }
}