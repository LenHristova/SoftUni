namespace P01_BillsPaymentSystem.Menus.MainMenus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Contracts;
    using Contracts.Models;

    public class MainMenu : Menu
    {
        protected const string MainNavigationMessage = null;

        private readonly IEnumerable<string> menus;

        public MainMenu(IReader reader, IWriter writer, IMainController mainController)
        : base(reader, writer, mainController)
        {
            this.menus = GetAvaliableMenus();
            this.navigationMessage = MainNavigationMessage;
        }

        private static IEnumerable<string> GetAvaliableMenus() =>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(SubMainMenuAttribute)).Any())
                .Select(t => t.Name)
                .ToList();

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("What do you want to do?");
            this.writer.WriteSpecialMessage("Possible commands are: ");
            this.writer.WriteHelperMessage("[Commands are case insensitive.]");

            foreach (var menu in menus)
            {
                this.writer.WriteSpecialMessage($"--- {menu.Replace(nameof(Menu), "")}");
            }

            var input = this.reader.ReadLine().Trim() + nameof(Menu);
            var menuName = this.menus
                .FirstOrDefault(m => string.Equals(m, input, StringComparison.CurrentCultureIgnoreCase));

            if (menuName == null)
            {
                throw new ArgumentException("Invalid menu.");
            }

            return this.mainController.OpenMenu(menuName);
        }
    }
}
