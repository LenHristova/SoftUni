namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using Models.Enums;
    using Services.Contracts;
    using Utilities;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private readonly IAlbumTagService albumTagService;
        private readonly IUserService userService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService, IAlbumTagService albumTagService, IUserService userService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumTagService = albumTagService;
            this.userService = userService;
        }

        // AddTagTo <albumName> <tag>
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

            var album = data[0];
            var tag = data[1].ValidateOrTransform();

            var albumExists = this.albumService.Exists(album);
            var tagExists = this.tagService.Exists(tag);

            if (!albumExists || !tagExists)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            var albumId = this.albumService.ByName<Album>(album).Id;

            var albumRole = this.userService.LoggedInUser<User>()
                .AlbumRoles
                .FirstOrDefault(ar => ar.AlbumId == albumId);

            if (albumRole == null || albumRole.Role != Role.Owner)
            {
                throw new InvalidOperationException("You can add tag only to your own albums!");
            }

            var tagId = this.tagService.ByName<Tag>(tag).Id;

            this.albumTagService.AddTagTo(albumId, tagId);

            return $"Tag {tag} added to {album}!";
        }
    }
}
