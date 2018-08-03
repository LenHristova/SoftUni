namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;
        private readonly IUserService userService;

        public AddTownCommand(ITownService townService, IUserService userService)
        {
            this.townService = townService;
            this.userService = userService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            var isLogIn = this.userService.IsLogIn();
            if (!isLogIn)
            {
                throw new InvalidOperationException("Log in first!");
            }

            if (data.Length != 2)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var townName = data[0];
            var country = data[1];

            var townExists = this.townService.Exists(townName);

            if (townExists)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            this.townService.Add(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}
