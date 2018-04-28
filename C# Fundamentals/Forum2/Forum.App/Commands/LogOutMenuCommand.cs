using Forum.App.Contracts;
using Forum.App.Menus;

namespace Forum.App.Commands
{
    public class LogOutMenuCommand : ICommand
    {
        private readonly IMenuFactory menuFactory;
        private readonly ISession session;

        public LogOutMenuCommand(ISession session, IMenuFactory menuFactory)
        {
            this.menuFactory = menuFactory;
            this.session = session;
        }

        public IMenu Execute(params string[] args)
        {
            this.session.Reset();

            IMenu menu = this.menuFactory.CreateMenu(nameof(MainMenu));
            return menu;
        }
    }
}
