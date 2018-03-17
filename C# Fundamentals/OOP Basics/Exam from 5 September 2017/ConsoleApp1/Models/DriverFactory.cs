using System;
using System.Collections.Generic;
using System.Linq;

public static class DriverFactory
{
    public static Driver CreateDriver(List<string> parameters)
    {
        var type = parameters[0];
        var name = parameters[1];
        var hp = int.Parse(parameters[2]);
        var fuelAmount = double.Parse(parameters[3]);

        var tyreParameters = parameters.Skip(4).ToList();
        var tyre = TyreFactory.CreateTyre(tyreParameters);

        var car = new Car(hp, fuelAmount, tyre);

        switch (type)
        {
            case "Aggressive":
                    return new AggressiveDriver(name, car);

            case "Endurance":
                return new EnduranceDriver(name, car);

                default:
                    throw new NotImplementedException();
        }
    }
}