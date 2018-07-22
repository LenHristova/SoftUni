namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System;
    using Attributes;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    public class UserMenu : Menu
    {
        protected const string MainNavigationMessage = null;

        private readonly IEnumerable<string> menus;

        public UserMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel)
            : base(reader, writer, mainController, userViewModel)
        {
            this.menus = InitializeAvaliableMenus();
            this.navigationMessage = MainNavigationMessage;
        }

        private static IEnumerable<string> InitializeAvaliableMenus() =>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(SubUserMenuAttribute)).Any())
                .Select(t => t.Name)
                .Append("LogOutMenu")
                .ToList();

        public override IMenu ExecutePartial()
        {
            this.writer.WriteSpecialMessage("What do you want to do?");
            this.writer.WriteSpecialMessage("Possible commands are: ");
            this.writer.WriteHelperMessage("[Commands are case insensitive.]");

            foreach (var menu in this.menus)
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
