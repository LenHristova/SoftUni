namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using System;
    using Attributes;
    using Commands.UserCommands;
    using Contracts;
    using Contracts.Models;
    using Data.Models;
    using ViewModels;

    [SubUserMenu]
    public class WithdrawMenu : Menu
    {
        public WithdrawMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel) 
            : base(reader, writer, mainController, userViewModel)
        {
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("Please enter withdraw info: ");
            this.writer.WriteSpecialMessage($"Format: <{nameof(BankAccount)}> <{nameof(BankAccount.BankAccountId)}> <amount>");
            this.writer.WriteSpecialMessage("OR");
            this.writer.WriteSpecialMessage($"Format: <{nameof(CreditCard)}> <{nameof(CreditCard.CreditCardId)}> <amount>");

            var data = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return this.mainController.Execute(nameof(WithdrawCommand), data);
        }
    }
}
