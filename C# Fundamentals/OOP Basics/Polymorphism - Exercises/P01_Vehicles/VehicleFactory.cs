using System;
using P01_Vehicles.Models;

namespace P01_Vehicles
{
    public static class VehicleFactory
    {
        public static Vehicle CreateNewVehicle()
        {
            var vehicleCpecifications = Console.ReadLine()?.Split();
            if (vehicleCpecifications == null || vehicleCpecifications.Length != 3)
            {
                throw new ArgumentException("Vehicle specifications must contains 3 arguments: vehicle type, fuel quantity and fuel consumption per km");
            }

            try
            {
                var fuelQuantity = double.Parse(vehicleCpecifications[1]);
                var litersPerKm = double.Parse(vehicleCpecifications[2]);

                var vehicleType = vehicleCpecifications[0];
                switch (vehicleType)
                {
                    case nameof(Car):
                        return new Car(fuelQuantity, litersPerKm);
                    case nameof(Truck):
                        var fuelLossPercentage = 5;
                        return new Truck(fuelQuantity, litersPerKm, fuelLossPercentage);
                }
            }
            catch (FormatException)
            {
                throw new FormatException("Fuel quantity and fuel consumption per km must be numbers");
            }

            throw new ArgumentException("First input parameter must be valid vehicle type");
        }
    }
}