namespace P01_BillsPaymentSystem.Commands.UserCommands
{
    using System;
    using System.Globalization;
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;
    using Menus.UserMenus;

    public class AddCreditCardCommand : Command
    {
        public AddCreditCardCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            var date = args[0] + "/1";
            if (!DateTime.TryParseExact(date, "yyyy/M/d", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expirationDate))
            {
                throw new ArgumentException("Invalid expiration date format");
            }

            if (expirationDate < DateTime.Today)
            {
                throw new ArgumentException("Already expired date.");
            }

            var id = this.userService.AddNewCreditCard(expirationDate);

            var response = $"You successfully added new credit card with id {id}";

            var menu = this.menuFactory.CreateMenu(nameof(MessagePage), response);
            return menu;
        }
    }
}
