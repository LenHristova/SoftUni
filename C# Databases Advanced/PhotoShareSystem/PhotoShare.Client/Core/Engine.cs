namespace PhotoShare.Client.Core
{
	using System;
	using System.Data.SqlClient;
	using System.Threading;
	using Contracts;
	using Services.Contracts;

	public class Engine : IEngine
	{
	    private bool isRunning;
	    private readonly IReader reader;
	    private readonly IWriter writer;
	    private readonly ICommandInterpreter commandInterpreter;
	    private readonly IDatabaseInitializerService dbInitializer;

	    public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter, IDatabaseInitializerService dbInitializer)
	    {
	        this.reader = reader;
	        this.writer = writer;
	        this.commandInterpreter = commandInterpreter;
	        this.dbInitializer = dbInitializer;
	    }

	    public void Run()
	    {
	        this.isRunning = true;

	        this.dbInitializer.InitializeDatabase();

	        while (this.isRunning)
	        {
	            try
	            {
	                this.writer.Write("Enter command: ");
	                var data = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
	                var result = this.commandInterpreter.Read(data);
	                //this.writer.Clear();
	                this.writer.WriteLine(result);
	            }
	            catch (Exception exception) when (exception is SqlException || exception is ArgumentException ||
	                                              exception is InvalidOperationException)
	            {
	                this.writer.WriteErrorMessage(exception.Message);
	            }
	        }

	        for (var i = 5; i >= 0; i--)
	        {
	            this.writer.Clear();
	            this.writer.WriteLine($"The program will be closed after {i} seconds.");
	            Thread.Sleep(1000);
	        }
            this.writer.WriteLine("Good Bye!");
	    }

	    public void Stop() => this.isRunning = false;
    }
}