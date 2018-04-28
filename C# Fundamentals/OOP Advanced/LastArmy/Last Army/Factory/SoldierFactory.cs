using System;
using System.Linq;
using System.Reflection;

public class SoldierFactory : ISoldierFactory
{
    public ISoldier CreateSoldier(string soldierTypeName, string name, int age, double experience, double endurance)
    {
        var soldierType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == soldierTypeName);

        if (soldierType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.TYPE_NOT_FOUND, nameof(Soldier)));
        }

        if (!typeof(ISoldier).IsAssignableFrom(soldierType))
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_APPROPRIATE_TYPE, soldierTypeName, nameof(ISoldier)));
        }

        var ctorParams = new object[] {name, age, experience, endurance};
        var soldier = (ISoldier) Activator.CreateInstance(soldierType, ctorParams);

        return soldier;
    }
}