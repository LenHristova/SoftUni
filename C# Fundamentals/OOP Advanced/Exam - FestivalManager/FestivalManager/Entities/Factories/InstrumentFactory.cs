using System;
using System.Linq;
using System.Reflection;

namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;

	public class InstrumentFactory : IInstrumentFactory
	{
		public IInstrument CreateInstrument(string type)
		{
		    var instrumentType = Assembly.GetCallingAssembly()
		        .GetTypes()
		        .FirstOrDefault(t => t.Name == type);


		    if (instrumentType == null)
		    {
		        throw new ArgumentException("Instrument not found");
		    }

		    if (!typeof(IInstrument).IsAssignableFrom(instrumentType))
		    {
		        throw new ArgumentException($"{type} is not IInstrument");
		    }

		    var instrument = (IInstrument)Activator.CreateInstance(instrumentType);
		    return instrument;
        }
	}
}