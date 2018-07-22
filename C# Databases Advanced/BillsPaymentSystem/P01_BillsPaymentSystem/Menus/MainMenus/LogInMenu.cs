namespace P01_BillsPaymentSystem.Menus.MainMenus
{
    using System;
    using Attributes;
    using Commands.MainCommands;
    using Contracts;
    using Contracts.Models;

    [SubMainMenu]
    public class LogInMenu : Menu
    {
        public LogInMenu(IReader reader, IWriter writer, IMainController mainController) 
            : base(reader, writer, mainController)
        {
        }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("Please enter your email and password");
            this.writer.WriteSpecialMessage("Format: <Email> <Password>");

            var data = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);


            return this.mainController.Execute(nameof(LogInCommand), data);
        }
    }
}
