namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using Commands.UserCommands;
    using Contracts;
    using Contracts.Models;

    public class MessagePage : Menu
    {
        protected const string ResultPageNavigationMessage = "Press any key to return to the UserMenu.";

        private readonly string response;

        public MessagePage(IReader reader, IWriter writer, IMainController mainController, string response) 
            : base(reader, writer, mainController)
        {
            this.response = response;
            this.navigationMessage = ResultPageNavigationMessage;
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage(response);

            this.reader.ReadLine();

            return this.mainController.Execute(nameof(ResetCommand));
        }
    }
}
