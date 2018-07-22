namespace P01_BillsPaymentSystem.Commands.MainCommands
{
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class LogInCommand : Command
    {
        public LogInCommand(IUserService userService, IMenuFactory menuFactory)
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.EnsureEnoughtArgs(args, 2);

            var email = args[0];
            var password = args[1];

            this.userService.TryLogInUser(email, password);

            var menu = this.menuFactory.CreateMenu(nameof(UserMenu), userService.GetUserViewModel());
            return menu;
        }
    }
}
