
using System;
using System.Linq;
namespace FestivalManager.Core
{
	using System.Reflection;
	using Contracts;
	using Controllers;
	using Controllers.Contracts;
	using IO.Contracts;

	/// <summary>
	/// by g0shk0
	/// </summary>
	public class Engine : IEngine
	{
	    private IReader reader;
	    private IWriter writer;
		private IFestivalController festivalCоntroller;
		private ISetController setCоntroller;

	    public Engine(IReader reader, IWriter writer, IFestivalController festivalCоntroller, ISetController setCоntroller)
	    {
	        this.reader = reader;
	        this.writer = writer;
	        this.festivalCоntroller = festivalCоntroller;
	        this.setCоntroller = setCоntroller;
	    }

	    // дайгаз
		public void Run()
		{
		    string input;
		    while ((input = reader.ReadLine()) != "END") 
			{
				try
				{
					string.Intern(input);

					var result = this.ProcessCommand(input);
					this.writer.WriteLine(result);
				}
				catch (Exception ex) 
				{
					this.writer.WriteLine("ERROR: " + ex.Message);
				}
			}

			var end = this.festivalCоntroller.ProduceReport().Trim();

			this.writer.WriteLine("Results:");
			this.writer.WriteLine(end);
		}

		public string ProcessCommand(string input)
		{
			var tokens = input.Split(" ".ToCharArray().First());

			var command = tokens.First();
			var args = tokens.Skip(1).ToArray();

			if (command == "LetsRock")
			{
				var setInfo = this.setCоntroller.PerformSets();
				return setInfo;
			}

			var festivalcontrolfunction = this.festivalCоntroller.GetType()
				.GetMethods()
				.FirstOrDefault(x => x.Name == command);

			string output;

			try
			{
				output = (string)festivalcontrolfunction.Invoke(this.festivalCоntroller, new object[] { args });
			}
			catch (TargetInvocationException e)
			{
				throw e.InnerException;
			}

			return output;
		}
	}
}