namespace P01_BillsPaymentSystem.Commands.UserCommands
{
    using System;
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class PayBillsCommand : Command
    {
        public PayBillsCommand(IUserService userService, IMenuFactory menuFactory)
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.EnsureEnoughtArgs(args, 1);

            if (!decimal.TryParse(args[0], out var amount))
            {
                throw new ArgumentException("Invalid amount format!");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive!");
            }

            this.userService.TryToPayBills(amount);

            var response = "All bills was paid successfully.";

            var menu = this.menuFactory.CreateMenu(nameof(MessagePage), response);
            return menu;
        }
    }
}
