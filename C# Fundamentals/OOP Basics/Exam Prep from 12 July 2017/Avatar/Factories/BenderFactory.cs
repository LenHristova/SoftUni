using System;
using System.Collections.Generic;

public static class BenderFactory
{
    public static Bender CreateBender(List<string> arguments)
    {
        var type = arguments[0];
        var name = arguments[1];
        var power = int.Parse(arguments[2]);
        var secondaryParameter = double.Parse(arguments[3]);

        switch (type)
        {
            case "Air":
                return new AirBender(name, power, secondaryParameter);
            case "Water":
                return new WaterBender(name, power, secondaryParameter);
            case "Fire":
                return new FireBender(name, power, secondaryParameter);
            case "Earth":
                return new EarthBender(name, power, secondaryParameter);
                default:
                    throw new NotImplementedException();
        }
    }
}