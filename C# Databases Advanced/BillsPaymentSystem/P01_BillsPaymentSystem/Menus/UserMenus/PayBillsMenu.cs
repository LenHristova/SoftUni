namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using Attributes;
    using Commands.UserCommands;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    [SubUserMenu]
    public class PayBillsMenu : Menu
    {
        public PayBillsMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel) 
            : base(reader, writer, mainController, userViewModel)
        {
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("Please enter bills amount: ");
            var billsAmount = this.reader.ReadLine();

            return this.mainController.Execute(nameof(PayBillsCommand), billsAmount);
        }
    }
}
