namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using Attributes;
    using Commands.UserCommands;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    [SubUserMenu]
    public class AddCreditCardMenu : Menu
    {
        public AddCreditCardMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel)
            : base(reader, writer, mainController, userViewModel)
        {
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage($"You are on the way to add new credit card.");
            this.writer.WriteSpecialMessage($"Your new credit card will be with default limit: $500");
            this.writer.WriteLine();
            this.writer.WriteSpecialMessage($"Please enter expiration date in format <yyyy/M>.");

            var data = this.reader.ReadLine().Trim();

            return this.mainController.Execute(nameof(AddCreditCardCommand), data);
        }
    }
}
