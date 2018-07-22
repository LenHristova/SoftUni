namespace P01_BillsPaymentSystem.Commands.MainCommands
{
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class SignUpCommand : Command
    {
        public SignUpCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.EnsureEnoughtArgs(args, 4);

            var firstName = args[0];
            var lastName = args[1];
            var email = args[2];
            var password = args[3];

            this.userService.TrySignUpUser(firstName, lastName, email, password);

            var menu = this.menuFactory.CreateMenu(nameof(UserMenu), this.userService.GetUserViewModel());
            return menu;
        }
    }
}
