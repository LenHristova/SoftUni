using System;

public static class VehicleFactory
{
    public static Vehicle CreateNewVehicle()
    {
        var vehicleCpecifications = Console.ReadLine()?.Split();
        if (vehicleCpecifications == null || vehicleCpecifications.Length != 4)
        {
            throw new ArgumentException("Vehicle specifications must contains 4 arguments");
        }

        try
        {
            var fuelQuantity = double.Parse(vehicleCpecifications[1]);
            var litersPerKm = double.Parse(vehicleCpecifications[2]);
            var tankCapacity = double.Parse(vehicleCpecifications[3]);

            var vehicleType = vehicleCpecifications[0];
            switch (vehicleType)
            {
                case nameof(Car):
                    return new Car(fuelQuantity, litersPerKm, tankCapacity);
                case nameof(Truck):
                    return new Truck(fuelQuantity, litersPerKm, tankCapacity);
                case nameof(Bus):
                    return new Bus(fuelQuantity, litersPerKm, tankCapacity);
            }
        }
        catch (FormatException)
        {
            throw new FormatException("Fuel quantity and fuel consumption per km must be numbers");
        }

        throw new ArgumentException("First input parameter must be valid vehicle type");
    }
}