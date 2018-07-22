namespace P01_BillsPaymentSystem.Menus.UserMenus
{
	using System;
	using Attributes;
	using Commands.UserCommands;
	using Contracts;
	using Contracts.Models;
	using ViewModels;

    [SubUserMenu]
    public class AddBankAccountMenu : Menu
    {
        public AddBankAccountMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel) 
            : base(reader, writer, mainController, userViewModel)
        {
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage($"You are on the way to add new bank account.");
            this.writer.WriteLine();
            this.writer.WriteSpecialMessage($"Please enter bank account info.");
            this.writer.WriteSpecialMessage($"Format: <bankName> <SWIFT>.");

            var data = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return this.mainController.Execute(nameof(AddBankAccountCommand), data);
        }
    }
}
