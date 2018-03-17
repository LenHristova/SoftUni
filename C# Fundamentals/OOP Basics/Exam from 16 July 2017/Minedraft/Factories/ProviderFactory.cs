using System.Collections.Generic;

public class ProviderFactory
{
    public Provider CreateProvider(IList<string> parameters)
    {
        var type = parameters[0];
        var id = parameters[1];
        var energyOutput = double.Parse(parameters[2]);

        switch (type)
        {
            case "Solar":
                return new SolarProvider(id, energyOutput);
            case "Pressure":
                return new PressureProvider(id, energyOutput);
            default:
                throw new InvalidCommandExeption();
        }
    }
}