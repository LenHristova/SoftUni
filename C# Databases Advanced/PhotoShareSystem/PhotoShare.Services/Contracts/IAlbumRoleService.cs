namespace PhotoShare.Services.Contracts
{
    using Models;
    using Models.Enums;

    public interface IAlbumRoleService
    {
        bool Exists(int albumId, int userId);

        AlbumRole PublishAlbumRole(int albumId, int userId, Role role);

        AlbumRole ChangeAlbumRole(int albumId, int userId, Role role);
    }
}
