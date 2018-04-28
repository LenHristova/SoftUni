using System;
using System.Linq;
using System.Reflection;

public class AmmunitionFactory : IAmmunitionFactory
{
    public IAmmunition CreateAmmunition(string ammunitionName)
    {
        var ammunitionType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == ammunitionName);

        if (ammunitionType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.TYPE_NOT_FOUND, nameof(Ammunition)));
        }

        if (!typeof(IAmmunition).IsAssignableFrom(ammunitionType))
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_APPROPRIATE_TYPE, ammunitionName, nameof(IAmmunition)));
        }

        var ammunition = (IAmmunition) Activator.CreateInstance(ammunitionType);

        return ammunition;
    }
}