namespace PhotoShare.Client.Core.Commands
{
	using System;
	using System.Linq;
	using Contracts;
	using Dtos;
	using Models;
	using Models.Enums;
	using Services.Contracts;

    public class ChangeAlbumRoleCommand : ICommand
    {            
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly IAlbumRoleService albumRoleService;

        public ChangeAlbumRoleCommand(IAlbumService albumService, IUserService userService, IAlbumRoleService albumRoleService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.albumRoleService = albumRoleService;
        }

        // ChangeAlbumRole <albumId> <username> <permission>
        // For example:
        // ChangeAlbumRole 4 dragon321 Owner
        // ChangeAlbumRole 4 dragon11 Viewer
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

            if (!int.TryParse(data[0], out var albumId))
            {
                throw new ArgumentException("Ivalid id format!");
            }

            var albumExists = this.albumService.Exists(albumId);
            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            var username = data[1];
            var userExists = this.userService.Exists(username);
            var isUserDeleted = this.userService.ByUsername<UserDto>(username)?.IsDeleted;

            if (!userExists || isUserDeleted == true)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var albumRole = this.userService.LoggedInUser<User>()
                .AlbumRoles
                .FirstOrDefault(ar => ar.AlbumId == albumId);

            if (albumRole == null || albumRole.Role != Role.Owner)
            {
                throw new InvalidOperationException("You can modify only your own albums!");
            }

            var validRole = Enum.TryParse(data[2], true, out Role role);
            if (!validRole)
            {
                throw new ArgumentException("Permission must be either \"Owner\" or \"Viewer\"!");
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;

            var isUserAddToAlbum = this.albumRoleService.Exists(albumId, userId);

            if (!isUserAddToAlbum)
            {
                throw new ArgumentException($"User {username} is not added to album {albumId}!");
            }

            this.albumRoleService.ChangeAlbumRole(albumId, userId, role);

            var albumTitle = this.albumService.ById<AlbumDto>(albumId).Name;

            return $"User {username}'s role for album {albumTitle} was changed to {role}!";
        }
    }
}
