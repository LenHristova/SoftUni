namespace TeamBuilder.App.Core
{
    using System;
    using System.Threading;
    using Contracts;
    using Services.Contracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IDbInitializeService dbInitializeService;
        private bool isRunning;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandDispatcher commandDispatcher, 
            IDbInitializeService dbInitializeService)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandDispatcher = commandDispatcher;
            this.dbInitializeService = dbInitializeService;
        }

        public void Run()
        {
            this.isRunning = true;
            this.dbInitializeService.Initialize();

            while (this.isRunning)
            {
                try
                {
                    this.writer.Write("Enter command: ");
                    var input = this.reader.ReadLine();
                    var output = this.commandDispatcher.Dispatch(input);

                    this.writer.WriteLine(output);
                }
                catch (Exception e)
                {
                    this.writer.WriteErrorMessage(e.GetBaseException().Message);
                }
            }
        }

        public void Stop()
        {
            this.writer.WriteLine("The program was closed!");
            this.writer.WriteLine("Good Bye!");

            this.isRunning = false;
        }
    }
}
