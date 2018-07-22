namespace P01_BillsPaymentSystem.Menus.MainMenus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Commands.MainCommands;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    [SubMainMenu]
    public class SignUpMenu : Menu
    {
        private readonly IEnumerable<string> requiredProperties;

        public SignUpMenu(IReader reader, IWriter writer, IMainController mainController)
            : base(reader, writer, mainController)
        {
            this.requiredProperties = InitializeRequiredProperties();
        }

        private static IEnumerable<string> InitializeRequiredProperties() =>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == nameof(UserSignUpView))
                ?.GetProperties()
                .Select(p => p.Name);

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("Please enter SignUp info.");
            this.writer.WriteSpecialMessage($"Format: <{string.Join("> <", requiredProperties)}>");

            var data = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return this.mainController.Execute(nameof(SignUpCommand), data);
        }
    }
}
