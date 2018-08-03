namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            var isLogin = this.userService.IsLogIn();
            if (isLogin)
            {
                throw new ArgumentException("You should log out first!");
            }

            if (data.Length != 4)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];
            var password = data[1];
            var repeatPassword = data[2];
            var email = data[3];

            var registerUserDto = new RegisterUserDto
            {
                Username = username,
                Password = password,
                Email = email
            };

            var isValid = this.IsValid(registerUserDto);
            if (!isValid)
            {
                throw new ArgumentException("Invalid data!");
            }

            var userExists = this.userService.Exists(username);
            if (userExists)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            this.userService.Register(username, password, email);

            return $"User {username} was registered successfully!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
