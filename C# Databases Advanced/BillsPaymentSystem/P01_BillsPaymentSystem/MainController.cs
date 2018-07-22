namespace P01_BillsPaymentSystem
{
    using System.Linq;
    using Contracts;
    using Contracts.Factories;
    using Contracts.Models;
    using Menus.MainMenus;

    public class MainController : IMainController
    {
        private readonly ICommandFactory commandFactory;
        private readonly IMenuFactory menuFactory;
        private readonly ISession session;

        public MainController(ICommandFactory commandFactory, IMenuFactory menuFactory, ISession session)
        {
            this.commandFactory = commandFactory;
            this.menuFactory = menuFactory;
            this.session = session;
        }

        public IMenu OpenMenu(string menuName)
        {
            if (menuName.ToLower() == "back")
            {
                return this.session.Back();
            }

            if (menuName.ToLower() == "logoutmenu")
            {
                this.session.LogOut();
                return this.OpenMenu(nameof(MainMenu));
            }

            var menu = this.menuFactory.CreateMenu(menuName, this.session.UserViewModel);
            this.session.PushView(menu);

            return menu;
        }

        public IMenu Execute(string commandName, params string[] data)
        {
            if (data.FirstOrDefault()?.ToLower() == "back")
            {
                return this.session.Back();
            }

            var command = this.commandFactory.CreateCommand(commandName);
            var menu = command.Execute(data);

            this.session.PushView(menu);

            return menu;
        }
    }
}
