namespace IRunes.Services.Contracts
{
    using Models.Tracks;

    public interface ITrackService
    {
        string Create(string name, string videoUrl, decimal price, string albumId);
        TrackModel ById(string id);
    }
}
