namespace PhotoShare.Client.Core.Commands
{
	using System;
	using Contracts;
	using Dtos;
	using Services.Contracts;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // Login <username> <password> 
        public string Execute(string[] data)
        {
            var isLogin = this.userService.IsLogIn();
            if (isLogin)
            {
                throw new ArgumentException("You should log out first!");
            }

            if (data.Length != 2)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];
            var password = data[1];

            var exists = this.userService.Exists(username);
            var isUserDeleted = this.userService.ByUsername<UserDto>(username)?.IsDeleted;
            var userPassword = this.userService.ByUsername<LogInUserDto>(username)?.Password;

            if (!exists || isUserDeleted == true || password != userPassword)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            this.userService.Login(username);

            return $"User {username} successfully logged in!";
        }
    }
}
