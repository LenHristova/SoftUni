using System;
using System.Collections.Generic;

public static class MonumentFactory
{
    public static Monument CreateMonument(List<string> arguments)
    {
        var type = arguments[0];
        var name = arguments[1];
        var affinity = int.Parse(arguments[2]);

        switch (type)
        {
            case "Air":
                return new AirMonument(name, affinity);
            case "Water":
                return new WaterMonument(name, affinity);
            case "Fire":
                return new FireMonument(name, affinity);
            case "Earth":
                return new EarthMonument(name, affinity);
            default:
                    throw new NotImplementedException();
        }
    }
}