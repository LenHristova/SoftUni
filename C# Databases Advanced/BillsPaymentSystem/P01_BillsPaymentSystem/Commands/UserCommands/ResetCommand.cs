namespace P01_BillsPaymentSystem.Commands.UserCommands
{
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class ResetCommand : Command
    {
        public ResetCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.userService.Reset();

            var menu = this.menuFactory.CreateMenu(nameof(UserMenu), this.userService.GetUserViewModel());
            return menu;
        }
    }
}
