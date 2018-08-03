namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class PrintFriendsListCommand : ICommand
    {
        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //PrintFriendsList <username>
        public string Execute(string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];

            var exists = this.userService.Exists(username);

            if (!exists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friendsAdded = this.userService.ByUsername<UserFriendsDto>(username).Friends;

            var friends = new List<FriendDto>();
            foreach (var friendDto in friendsAdded)
            {
                if (friendDto.IsDeleted)
                {
                    continue;
                }

                var friendFriendsAdded = this.userService.ByUsername<UserFriendsDto>(friendDto.Username).Friends;
                var hasFriendship = friendFriendsAdded.Any(f => f.Username == username);

                if (hasFriendship)
                {
                    friends.Add(friendDto);
                }
            }

            if (!friends.Any())
            {
                return "No friends for this user. :(";
            }

            var sb = new StringBuilder();
            sb.AppendLine("Friends:");
            foreach (var friendDto in friends.OrderBy(f => f.Username))
            {
                sb.AppendLine($" -{friendDto.Username}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
