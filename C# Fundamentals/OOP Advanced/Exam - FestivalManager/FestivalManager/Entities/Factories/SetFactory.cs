using System;
using System.Linq;
using System.Reflection;

namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;

	public class SetFactory : ISetFactory
	{
		public ISet CreateSet(string name, string type)
		{
		    var setType = Assembly.GetCallingAssembly()
		        .GetTypes()
		        .FirstOrDefault(t => t.Name == type);

		    if (setType == null)
		    {
		        throw new ArgumentException("Set not found");
		    }

		    if (!typeof(ISet).IsAssignableFrom(setType))
		    {
		        throw new ArgumentException($"{type} is not ISet");
            }

		    var set = (ISet)Activator.CreateInstance(setType, name);
		    return set;
		}
	}
}
