namespace PhotoShare.Client.Core.Commands
{
	using System;
	using Contracts;
	using Dtos;
	using Services.Contracts;

    public class LogoutCommand : ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // Logout 
        public string Execute(string[] data)
        {
            var isLogin = this.userService.IsLogIn();
            if (!isLogin)
            {
                throw new ArgumentException("You should log in first in order to logout!");
            }

            var username = this.userService.LoggedInUser<UserDto>().Username;

            this.userService.Logout();

            return $"User {username} successfully logged out!";
        }
    }
}
