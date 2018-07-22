using Employees.App.Core.Contracts;

namespace Employees.App.Commands
{
    using Contracts;

    internal class ExitCommand : ICommand
    {
        private readonly IEngine engine;

        public ExitCommand(IEngine engine)
        {
            this.engine = engine;
        }

        public string Execute(params string[] args)
        {
            this.engine.Stop();
            return null;
        }
    }
}
