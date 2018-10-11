namespace IRunes.Web.Controllers
{
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Linq;
    using ViewModels.Albums;

    public class AlbumsController : Controller
    {
        private readonly IAlbumService albums;

        public AlbumsController(IHttpRequest httpRequest)
            : base(httpRequest)
        {
            this.albums = new AlbumService();
        }

        public IHttpResponse All()
        {
            if (!this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            var albumsList = this.albums.All().ToList();

            string albumsResult;

            if (albumsList.Any())
            {
                albumsResult = string.Join(
                    Environment.NewLine,
                    albumsList
                        .Select(a => $"<div><a href=\"/Albums/Details?id={a.Id}\">{a.Name}</a></div>"));
            }
            else
            {
                albumsResult = "There are currently no albums.";
            }

            this.ViewBag["albums"] = albumsResult;

            return this.View();
        }

        public IHttpResponse Create()
            => !this.IsAuthenticated ? new RedirectResult("/") : this.View();


        public IHttpResponse Create(CreateAlbumViewModel model)
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

            var albumId = this.albums.Create(model.Name, model.CoverImageUrl);

            if (albumId ==  null)
            {
                return this.ServerError("Oops.. something happened...");
            }

            return new RedirectResult($"/Albums/Details?id={albumId}");
        }

        public IHttpResponse Details(string id)
        {
            if (!this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            var album = this.albums.ById(id);

            this.ViewBag["name"] = album.Name;
            this.ViewBag["coverImageUrl"] = album.CoverImageUrl;
            this.ViewBag["price"] = album.Price.ToString("F2");

            string tracksResult;

            if (album.Tracks.Any())
            {
                tracksResult = string.Join(
                    Environment.NewLine,
                    album.Tracks
                        .Select((t, i) => $"<li>" +
                                          $"<span class=\"font-weight-bold\">{i + 1}</span>. " +
                                          $"<a class=\"font-italic\" href=\"/Tracks/Details?albumId={id}&trackId={t.Id}\">{t.Name}</a>" +
                                          $"</li>"));
            }
            else
            {
                tracksResult = "There are currently no tracks.";
            }

            this.ViewBag["tracks"] = tracksResult;
            this.ViewBag["albumId"] = id;

            return this.View();
        }
    }
}
