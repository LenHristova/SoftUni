namespace IRunes.Web
{
    using Controllers;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Results;
    using SIS.WebServer.Routing;
    using ViewModels.Account;
    using ViewModels.Albums;
    using ViewModels.Tracks;

    internal class Launcher
    {
        private static void Main(string[] args)
        {
            var serverRoutingTable = new ServerRoutingTable();

            RegisterRoutes(serverRoutingTable);

            var server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void RegisterRoutes(ServerRoutingTable serverRoutingTable)
        {
            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/"] = req => new HomeController(req).Index();

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Home/Index"] = req => new RedirectResult("/");

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Users/Register"] = req => new AccountController(req).Register();

            serverRoutingTable
                .Routes[HttpRequestMethod.Post]["/Users/Register"] = req => new AccountController(req).Register(
                new RegisterUserViewModel
                {
                    Username = req.FormData["username"]?.ToString()?.Trim(),
                    Password = req.FormData["password"]?.ToString(),
                    ConfirmPassword = req.FormData["confirmPassword"]?.ToString(),
                    Email = req.FormData["email"]?.ToString()?.Trim()
                });

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Users/Login"] = req => new AccountController(req).Login();

            serverRoutingTable
                .Routes[HttpRequestMethod.Post]["/Users/Login"] = req => new AccountController(req).Login(
                new LoginUserViewModel
                {
                    UsernameOrEmail = req.FormData["usernameOrEmail"]?.ToString()?.Trim(),
                    Password = req.FormData["password"]?.ToString()
                });

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Users/Logout"] = req => new AccountController(req).Logout();

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Albums/All"] = req => new AlbumsController(req).All();

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Albums/Create"] = req => new AlbumsController(req).Create();

            serverRoutingTable
                .Routes[HttpRequestMethod.Post]["/Albums/Create"] = req => new AlbumsController(req).Create(
                new CreateAlbumViewModel
                {
                    Name = req.FormData["name"]?.ToString()?.Trim(),
                    CoverImageUrl = req.FormData["coverImageUrl"]?.ToString()?.Trim()
                });

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Albums/Details"] = req => new AlbumsController(req).Details(
                req.QueryData["id"]?.ToString());

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Tracks/Create"] = req => new TracksController(req).Create(
                req.QueryData["albumId"]?.ToString());

            serverRoutingTable
                .Routes[HttpRequestMethod.Post]["/Tracks/Create"] = req => new TracksController(req).Create(
                new CreateTrackViewModel
                {
                    Name = req.FormData["name"]?.ToString()?.Trim(),
                    VideoUrl = req.FormData["videoUrl"]?.ToString()?.Trim(),
                    Price = decimal.TryParse(req.FormData["price"].ToString(), out var price) ? price : default(decimal)
                },
                req.QueryData["albumId"]?.ToString());

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/Tracks/Details"] = req => new TracksController(req).Details(
                req.QueryData["albumId"]?.ToString(), req.QueryData["trackId"]?.ToString());
        }
    }
}
