using Forum.App.Contracts;

namespace Forum.App.Commands.PaginationCommands
{
    public class BackCommand : PaginationCommand
    {
        public BackCommand(ISession session) : base(session)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            IMenu menu = this.session.Back();
            return menu;
        }
    }
}
