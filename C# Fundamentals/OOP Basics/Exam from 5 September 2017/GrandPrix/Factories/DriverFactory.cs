using System;
using System.Collections.Generic;
public class DriverFactory
{
    public Driver CreateDriver(string type, string name, Car car)
    {
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