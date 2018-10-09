namespace IRunes.Web.Controllers
{
    using System;
    using System.Linq;
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using ViewModels.Tracks;

    public class TracksController : Controller
    {
        private readonly ITrackService tracks;
        private readonly IAlbumService albums;

        public TracksController(IHttpRequest httpRequest)
            : base(httpRequest)
        {
            this.tracks = new TrackService();
            this.albums = new AlbumService();
        }

        public IHttpResponse Create(string albumId)
        {
            if (!this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            var albumExists = this.albums.Exists(albumId);
            if (!albumExists)
            {
                return this.NotFoundError();
            }

            this.ViewBag["albumId"] = albumId;

            return this.View();
        }

        public IHttpResponse Create(CreateTrackViewModel model, string albumId)
        {
            if (!this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            if (!this.IsValid(model, out var results))
            {
                this.AddErrorMessageToViewData(string.Join("<br />", results));

                return this.View();
            }

            var albumExists = this.albums.Exists(albumId);
            if (!albumExists)
            {
                return this.NotFoundError();
            }

            var trackId = this.tracks.Create(
                model.Name, model.VideoUrl, model.Price, albumId);

            if (trackId == null)
            {
                return this.ServerError("Oops.. something happened...");
            }

            return new RedirectResult($"/Tracks/Details?albumId={albumId}&trackId={trackId}");
        }

        public IHttpResponse Details(string albumId, string trackId)
        {
            if (!this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            var track = this.tracks.ById(trackId);

            this.ViewBag["name"] = track.Name;
            this.ViewBag["videoUrl"] = track.VideoUrl.Split("=").LastOrDefault();
            this.ViewBag["price"] = track.Price.ToString("F2");

            var albumExists = this.albums.Exists(albumId);
            if (!albumExists)
            {
                return this.NotFoundError();
            }

            this.ViewBag["albumId"] = albumId;

            return this.View();
        }
    }
}
