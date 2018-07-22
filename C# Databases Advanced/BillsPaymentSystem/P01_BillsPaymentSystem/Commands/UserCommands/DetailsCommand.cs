namespace P01_BillsPaymentSystem.Commands.UserCommands
{
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class DetailsCommand : Command
    {
        public DetailsCommand(IUserService userService, IMenuFactory menuFactory)
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            var userViewModel = this.userService.GetUserViewModel();

            var menu = this.menuFactory.CreateMenu(nameof(DetailsMenu), userViewModel);
            return menu;
        }
    }
}
