using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class HarvesterFactory : IHarvesterFactory
{
    public IHarvester GenerateHarvester(IList<string> args)
    {
        string harvesterName = args[0];

        var id = int.Parse(args[1]);
        var oreOutput = double.Parse(args[2]);
        var energyReq = double.Parse(args[3]);

        var harvesterType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == harvesterName + nameof(Harvester));

        if (harvesterType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_FOUND, nameof(Harvester)));
        }

        if (!typeof(IHarvester).IsAssignableFrom(harvesterType))
        {
            throw new InvalidOperationException(string.Format(OutputMessages.NOT_ASSIGNABLE_FROM, harvesterType, nameof(IHarvester)));
        }

        var ctors = harvesterType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        var harvester = (IHarvester)ctors[0].Invoke(new object[] { id, oreOutput, energyReq });

        return harvester;
    }
}