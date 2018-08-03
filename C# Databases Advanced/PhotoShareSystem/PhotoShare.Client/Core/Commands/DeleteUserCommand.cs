namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            var isLogIn = this.userService.IsLogIn();
            if (!isLogIn)
            {
                throw new InvalidOperationException("Log in first!");
            }

            if (data.Length != 1)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];

            var userExists = this.userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var isAutorized = this.userService.IsLogIn(username);

            if (!isAutorized)
            {
                throw new InvalidOperationException("You can delete only your own profile!");
            }

            //var isUserDeleted = this.userService.ByUsername<UserDto>(username).IsDeleted;

            //if (isUserDeleted.HasValue && isUserDeleted.Value == true)
            //{
            //    throw new InvalidOperationException($"User {username} is already deleted!");
            //}

            this.userService.Delete(username);

            return $"User {username} was deleted from the database!";
        }
    }
}
