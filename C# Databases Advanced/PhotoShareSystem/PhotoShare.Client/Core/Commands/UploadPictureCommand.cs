namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Dtos;
    using Contracts;
    using Models;
    using Models.Enums;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private readonly IPictureService pictureService;
        private readonly IAlbumService albumService;
        private readonly IUserService userService;

        public UploadPictureCommand(IPictureService pictureService, IAlbumService albumService, IUserService userService)
        {
            this.pictureService = pictureService;
            this.albumService = albumService;
            this.userService = userService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
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

            var albumName = data[0];
            var pictureTitle = data[1];
            var path = data[2];

            var albumExists = this.albumService.Exists(albumName);

            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var albumId = this.albumService.ByName<AlbumDto>(albumName).Id;

            var albumRole = this.userService.LoggedInUser<User>()
                .AlbumRoles
                .FirstOrDefault(ar => ar.AlbumId == albumId);

            if (albumRole == null || albumRole.Role != Role.Owner)
            {
                throw new InvalidOperationException("You can upload picture only to your own albums!");
            }

            this.pictureService.Create(albumId, pictureTitle, path);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
