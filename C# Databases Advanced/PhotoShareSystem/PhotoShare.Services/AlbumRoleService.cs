namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Models;
    using Models.Enums;
    using Data;
    using Contracts;

    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public bool Exists(int albumId, int userId)
            => this.context.AlbumRoles
                .Any(ar => ar.AlbumId == albumId && ar.UserId == userId);

        public AlbumRole PublishAlbumRole(int albumId, int userId, Role role)
        {
            var albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = role
            };

            this.context.AlbumRoles.Add(albumRole);

            this.context.SaveChanges();

            return albumRole;
        }

        public AlbumRole ChangeAlbumRole(int albumId, int userId, Role role)
        {
            var albumRole = this.context.AlbumRoles
                .Single(ar => ar.AlbumId == albumId && ar.UserId == userId);

            albumRole.Role = role;

            this.context.SaveChanges();

            return albumRole;
        }
    }
}
