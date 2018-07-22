namespace P01_BillsPaymentSystem.Commands.UserCommands
{
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class AddBankAccountCommand : Command
    {
        public AddBankAccountCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.EnsureEnoughtArgs(args, 2);

            var bankName = args[0];
            var swift = args[1];

            var id = this.userService.AddNewBankAccount(bankName, swift);

            var response = $"You successfully added new bank account with id {id}";

            var menu = this.menuFactory.CreateMenu(nameof(MessagePage), response);
            return menu;
        }
    }
}
