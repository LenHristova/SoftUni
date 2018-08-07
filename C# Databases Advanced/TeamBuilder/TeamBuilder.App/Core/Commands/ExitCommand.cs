namespace TeamBuilder.App.Core.Commands
{
    using Contracts;
    using Utilities;

    public class ExitCommand : ICommand
    {
        private readonly IEngine engine;

        public ExitCommand(IEngine engine)
        {
            this.engine = engine;
        }

        // Exit
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            engine.Stop();
            return null;
        }
    }
}
