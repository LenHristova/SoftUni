namespace BusTickets.Client.Core.Commands
{
    using Contracts;

    public class ExitCommand : ICommand
    {
        private readonly IEngine engine;

        public ExitCommand(IEngine engine)
        {
            this.engine = engine;
        }

        public string Execute(params string[] data)
        {
            this.engine.Stop();
            return null;
        }
    }
}
