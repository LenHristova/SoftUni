namespace SIS.Demo
{
    using HTTP.Enums;
    using WebServer;
    using WebServer.Routing;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable
                .Routes[HttpRequestMethod.Get]["/"] = req => new HomeController().Index();

            var server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
