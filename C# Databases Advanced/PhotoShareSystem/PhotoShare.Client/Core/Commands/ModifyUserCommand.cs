namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;

        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var isLogIn = this.userService.IsLogIn();
            if (!isLogIn)
            {
                throw new InvalidOperationException("Log in first!");
            }

            if (data.Length != 3)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];
            var property = data[1];
            var value = data[2];

            //var userExists = this.userService.Exists(username);
            //var isUserDeleted = this.userService.ByUsername<User>(username)?.IsDeleted;

            //if (!userExists || isUserDeleted == true)
            //{
            //    throw new ArgumentException($"User {username} not found");
            //}

            var isAutorized = this.userService.IsLogIn(username);

            if (!isAutorized)
            {
                throw new InvalidOperationException("You can modify only your own profile!");
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;
            try
            {
                SetProperty(property, value, userId);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"Value {value} not valid!{Environment.NewLine}" +
                                            $"{e.Message}");
            }


            return $"User {username} {property} is {value}.";
        }

        private void SetProperty(string property, string value, int userId)
        {
            switch (property.ToLower())
            {
                case "password":
                    {
                        this.ChangePassword(value, userId);
                        break;
                    }
                case "borntown":
                    {
                        var townId = GetTownId(value);
                        this.userService.SetBornTown(userId, townId);
                        break;
                    }
                case "currenttown":
                    {
                        var townId = GetTownId(value);
                        this.userService.SetCurrentTown(userId, townId);
                        break;
                    }
                default:
                    throw new ArgumentException($"Property {property} not supported!");
            }
        }

        private int GetTownId(string value)
        {
            var townExists = this.townService.Exists(value);
            if (!townExists)
            {
                throw new ArgumentException($"Town {value} not found!");
            }

            var townId = this.townService.ByName<TownDto>(value).Id;
            return townId;
        }

        private void ChangePassword(string value, int userId)
        {
            var isValid = value.Any(char.IsLower) && value.Any(char.IsDigit);
            if (!isValid)
            {
                throw new ArgumentException("Invalid Password!");
            }

            this.userService.ChangePassword(userId, value);
        }
    }
}
