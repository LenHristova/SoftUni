using System;

public class StartUp
{
    static void Main()
    {
        try
        {
            var car = VehicleFactory.CreateNewVehicle();
            var truck = VehicleFactory.CreateNewVehicle();
            truck.FuelLossPercentage = 5;
            var bus = VehicleFactory.CreateNewVehicle();

            var actionCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < actionCount; i++)
            {
                try
                {
                    TryToPerformAction(car, truck, bus);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void TryToPerformAction(Vehicle car, Vehicle truck, Vehicle bus)
    {
        var actionArgs = Console.ReadLine()?.Trim().Split();
        if (actionArgs == null || actionArgs.Length != 3)
        {
            throw new ArgumentException("Action must contains 3 arguments.");
        }

        var actionType = actionArgs[0];
        var vehicleType = actionArgs[1];

        Vehicle currVehicle = GetVehicle(car, truck, bus, vehicleType);

        if (!double.TryParse(actionArgs[2], out var number))
        {
            throw new FormatException("Distance/Fuel must be number.");
        }

        PerformAction(actionType, currVehicle, number);
    }

    private static void PerformAction(string actionType, Vehicle currVehicle, double number)
    {
        switch (actionType)
        {
            case nameof(Vehicle.Drive):
                var distance = number;
                Console.WriteLine(currVehicle.Drive(distance));
                break;
            case nameof(Bus.DriveEmpty):
                if (currVehicle is Bus currentBus)
                {
                    distance = number;
                    Console.WriteLine(currentBus.DriveEmpty(distance));
                }
                break;
            case nameof(Vehicle.Refuel):
                var fuelAmount = number;
                currVehicle.Refuel(fuelAmount);
                break;
                default:
                    throw new ArgumentException("Invalid action type");
        }
    }

    private static Vehicle GetVehicle(Vehicle car, Vehicle truck, Vehicle bus, string vehicleType)
    {
        Vehicle currVehicle;
        switch (vehicleType)
        {
            case nameof(Car):
                currVehicle = car;
                break;
            case nameof(Truck):
                currVehicle = truck;
                break;
            case nameof(Bus):
                currVehicle = bus;
                break;
            default:
                throw new ArgumentException("Invalid vehicle type.");
        }

        return currVehicle;
    }
}