using System;

using Forum.App.Contracts;
using Forum.App.Menus;

namespace Forum.App.Commands
{
    public class SignUpCommand : ICommand
    {
        private readonly IMenuFactory menuFactory;
        private readonly IUserService userService;

        public SignUpCommand(IMenuFactory menuFactory, IUserService userService)
        {
            this.userService = userService;
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            string username = args?[0];
            string password = args?[1];
            bool succsess = this.userService.TrySignUpUser(username, password);
            if (!succsess)
            {
                throw new InvalidOperationException("Invalid Sign up!");
            }

            return this.menuFactory.CreateMenu(nameof(MainMenu));
        }
    }
}
