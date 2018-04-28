using Forum.App.Contracts;

namespace Forum.App.Commands.PaginationCommands
{
    public abstract class PaginationCommand : ICommand
    {
        protected readonly ISession session;

        protected PaginationCommand(ISession session)
        {
            this.session = session;
        }

        public abstract IMenu Execute(params string[] args);
    }
}