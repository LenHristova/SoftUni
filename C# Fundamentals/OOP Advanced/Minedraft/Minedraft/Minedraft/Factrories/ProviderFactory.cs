using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ProviderFactory : IProviderFactory
{
    public IProvider GenerateProvider(IList<string> args)
    {
        var id = int.Parse(args[1]);
        var type = args[0];
        var energyOutput = double.Parse(args[2]);

        var providerType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == type + nameof(Provider));

        if (providerType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_FOUND, nameof(Provider)));
        }

        if (!typeof(IProvider).IsAssignableFrom(providerType))
        {
            throw new InvalidOperationException(string.Format(OutputMessages.NOT_ASSIGNABLE_FROM, providerType, nameof(IProvider)));
        }

        var ctors = providerType
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        var provider = (IProvider)ctors[0].Invoke(new object[] { id, energyOutput });
        return provider;
    }
}