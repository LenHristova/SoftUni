namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Models;
    using Models.Enums;
    using Services.Contracts;
    using Utilities;


    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            var isLogIn = this.userService.IsLogIn();
            if (!isLogIn)
            {
                throw new InvalidOperationException("Log in first!");
            }

            if (data.Length < 3)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var username = data[0];
            var albumTitle = data[1];
            var bgColor = data[2];
            var tagsNames = data.Skip(3).ToArray();

            //var userExists = this.userService.Exists(username);
            //var isUserDeleted = this.userService.ByUsername<UserDto>(username)?.IsDeleted;

            //if (!userExists || isUserDeleted == true)
            //{
            //    throw new ArgumentException($"User {username} not found!");
            //}

            var isAutorized = this.userService.IsLogIn(username);

            if (!isAutorized)
            {
                throw new InvalidOperationException("You can create album only for your own profile!");
            }

            var albumExists = this.albumService.Exists(albumTitle);
            if (albumExists)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var validColor = Enum.TryParse<Color>(bgColor, true, out var color);
            if (!validColor)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            for (int i = 0; i < tagsNames.Length; i++)
            {
                tagsNames[i] = tagsNames[i].ValidateOrTransform();

                var tagsExists = this.tagService.Exists(tagsNames[i]);
                if (!tagsExists)
                {
                    throw new ArgumentException("Invalid tags!");
                }
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;

            var tagsIds = tagsNames.Select(t => this.tagService.ByName<TagDto>(t).Id);

            this.albumService.Create(userId, albumTitle, color, tagsIds);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
