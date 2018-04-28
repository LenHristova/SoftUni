using System;
using System.Collections.Generic;
using System.Text;

using Forum.App.Contracts;
using Forum.App.Menus;

namespace Forum.App.Commands
{
    public class LogInCommand : ICommand
    {
        private readonly IMenuFactory menuFactory;
        private readonly IUserService userService;

        public LogInCommand(IMenuFactory menuFactory, IUserService userService)
        {
            this.userService = userService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            string username = args?[0];
            string password = args?[1];
            bool succsess = this.userService.TryLogInUser(username, password);
            if (!succsess)
            {
                throw new InvalidOperationException("Invalid login!");
            }

            return this.menuFactory.CreateMenu(nameof(MainMenu));
        }
    }
}
