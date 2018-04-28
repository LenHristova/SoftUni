using System;
using System.Linq;
using System.Reflection;

using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core.Factories
{
    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitType)
        {

            var type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.Name == unitType);

            if (type == null)
            {
                throw new ArgumentException($"Invalid unit type: {unitType}!");
            }

            if (!typeof(IUnit).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{unitType} is not a Unit type!");
            }

            var unit = (IUnit)Activator.CreateInstance(type);
            return unit;
        }
    }
}
