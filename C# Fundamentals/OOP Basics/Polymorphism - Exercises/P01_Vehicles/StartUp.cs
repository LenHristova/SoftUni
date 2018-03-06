using System;
using P01_Vehicles.Models;

namespace P01_Vehicles
{
    public class StartUp
    {
        static void Main()
        {
            try
            {
                var car = VehicleFactory.CreateNewVehicle();
                var truck = VehicleFactory.CreateNewVehicle();

                var actionCount = int.Parse(Console.ReadLine());
                for (int i = 0; i < actionCount; i++)
                {
                    try
                    {
                        TryToPerformAction(car, truck);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                Console.WriteLine($"Car: {car.FuelQuantity:F2}");
                Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void TryToPerformAction(Vehicle car, Vehicle truck)
        {
            var actionArgs = Console.ReadLine()?.Split();
            if (actionArgs == null || actionArgs.Length != 3)
            {
                throw new ArgumentException("Action must contains 3 arguments.");
            }

            var actionType = actionArgs[0];
            var vehicleType = actionArgs[1];

            Vehicle vehicle = GetVehicleType(car, truck, vehicleType);

            PerformAction(actionArgs, actionType, vehicle);
        }

        private static void PerformAction(string[] actionArgs, string actionType, Vehicle vehicle)
        {
            if (double.TryParse(actionArgs[2], out var parameter))
            {
                switch (actionType)
                {
                    case nameof(Vehicle.Drive):
                        var distance = parameter;
                        Console.WriteLine(vehicle.Drive(distance));
                        break;
                    case nameof(Vehicle.Refuel):
                        var fuelAmount = parameter;
                        vehicle.Refuel(fuelAmount);
                        break;
                }
            }
            else
            {
                throw new FormatException("Fuel amount must be number.");
            }
        }

        private static Vehicle GetVehicleType(Vehicle car, Vehicle truck, string vehicleType)
        {
            Vehicle vehicle;
            switch (vehicleType)
            {
                case nameof(Car):
                    vehicle = car;
                    break;
                case nameof(Truck):
                    vehicle = truck;
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            return vehicle;
        }
    }
}