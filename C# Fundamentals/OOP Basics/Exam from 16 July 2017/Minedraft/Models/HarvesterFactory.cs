using System;
using System.Collections.Generic;

public static class HarvesterFactory
{
    public static Harvester CreateHarvester(IList<string> parameters)
    {
        var type = parameters[0];
        var id = parameters[1];
        var oreOutput = double.Parse(parameters[2]);
        var energyRequirement = double.Parse(parameters[3]);

        switch (type)
        {
            case "Sonic":
                var sonicFactor = int.Parse(parameters[4]);
                return new SonicHarvester(id, oreOutput, energyRequirement, sonicFactor);

            case "Hammer":
                return new HammerHarvester(id, oreOutput, energyRequirement);

            default:
                throw new NotSupportedException();
        }
    }
}
