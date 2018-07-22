namespace P01_BillsPaymentSystem.Menus
{
    using Attributes;
    using Commands;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    [SubMainMenu]
    [SubUserMenu]
    public class ExitMenu : Menu
    {
        public ExitMenu(IReader reader, IWriter writer, IMainController mainController)
            : base(reader, writer, mainController)
        {
            this.navigationMessage = null;
        }

        public ExitMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel)
            : base(reader, writer, mainController, userViewModel)
        {
            this.navigationMessage = null;
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("Are are you sure you want to exit from program? - <Yes> or <No>");

            var input = this.reader.ReadLine().Trim();

            while (true)
            {
                if (input.ToLower() == "yes")
                {
                    return this.mainController.Execute(nameof(ExitCommand));
                }
                else if (input.ToLower() == "no")
                {
                    return this.mainController.OpenMenu("Back");
                }

                this.writer.WriteErrorMessage("Unrecognised answer.");

                input = this.reader.ReadLine().Trim();
            }
        }
    }
}
