using System.Threading;

namespace Employees.App.Core
{
    using System;
    using System.Linq;
    using Contracts;
    using IO.Contracts;
    using Services.Contracts;

    internal class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandParser commandParser;
        private readonly IDbInitializerService dbInitializer;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser, IDbInitializerService dbInitializer)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
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
                    var input = this.reader.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var commandName = input.First();
                    var commandArgs = input.Skip(1).ToArray();

                    var command = this.commandParser.ParseCommand(commandName);
                    var output = command.Execute(commandArgs);

                    this.writer.Clear();
                    this.writer.WriteLine(output);
                }
                catch (Exception e)
                {
                    this.writer.WriteErrorMessage(e.Message);
                }
            }

            for (int i = 5; i >= 0; i--)
            {
                this.writer.Clear();
                this.writer.WriteLine($"The program will be closed after {i} seconds.");
                Thread.Sleep(1000);
            }
        }

        public void Stop() => this.isRunning = false;
    }
}
