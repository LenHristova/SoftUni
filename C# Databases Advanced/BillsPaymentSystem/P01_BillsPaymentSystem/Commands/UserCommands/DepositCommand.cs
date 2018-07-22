namespace P01_BillsPaymentSystem.Commands.UserCommands
{
	using System;
	using Contracts;
	using Contracts.Factories;
	using Contracts.Services;
	using Data.Models.Enums;
	using Menus.UserMenus;

    public class DepositCommand : Command
    {
        public DepositCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        { }

        public override IMenu Execute(params string[] args)
        {
            this.EnsureEnoughtArgs(args, 3);

            var type = args[0];
            if (!Enum.TryParse<PaymentMethodType>(type, true, out var paymentMethodType))
            {
                throw new ArgumentException("Invalid payment method type!");
            }

            if (!int.TryParse(args[1], out var id))
            {
                throw new ArgumentException("Invalid Id format!");
            }

            if (!decimal.TryParse(args[2], out var amount))
            {
                throw new ArgumentException("Invalid amount format!");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive!");
            }

            this.userService.TryToDeposit(paymentMethodType, id, amount);

            var response = $"${amount} was successfully added to {type} with id {id}.";

            var menu = this.menuFactory.CreateMenu(nameof(MessagePage), response);
            return menu;
        }
    }
}
