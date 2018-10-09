namespace IRunes.Services.Contracts
{
    using Models.Albums;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        string Create(string name, string coverImageUrl);

        IEnumerable<AlbumListingModel> All();

        AlbumModel ById(string id);

        bool Exists(string id);
    }
}
