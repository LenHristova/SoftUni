namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
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

            var userUsername = data[0];
            var friendUsername = data[1];

            //var userExists = this.userService.Exists(userUsername);
            //var isUserDeleted = this.userService.ByUsername<UserDto>(userUsername)?.IsDeleted;

            //if (!userExists || isUserDeleted == true)
            //{
            //    throw new ArgumentException($"User {userUsername} not found!");
            //}

            var isAutorized = this.userService.IsLogIn(userUsername);

            if (!isAutorized)
            {
                throw new InvalidOperationException("You can accept friend only for your own profile!");
            }

            var friendExists = this.userService.Exists(friendUsername);
            var isFriendDeleted = this.userService.ByUsername<UserDto>(friendUsername)?.IsDeleted;

            if (!friendExists || isFriendDeleted == true)
            {
                throw new ArgumentException($"User {friendUsername} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(userUsername);

            var friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            var userHasFriendship = user.Friends.Any(f => f.Username == friendUsername);

            var friendHasFriendship = friend.Friends.Any(f => f.Username == userUsername);

            if (userHasFriendship && friendHasFriendship)
            {
                throw new InvalidOperationException($"{friendUsername} is already a friend to {userUsername}.");
            }
            else if (!friendHasFriendship)
            {
                throw new InvalidOperationException($"{friendUsername} has not added {userUsername} as a friend.");
            }

            this.userService.AcceptFriend(user.Id, friend.Id);

            return $"User {userUsername} accepted {friendUsername} as a friend!";
        }
    }
}
