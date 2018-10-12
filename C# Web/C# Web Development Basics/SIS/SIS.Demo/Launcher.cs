namespace SIS.Demo
{
    using Framework;
    using Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var server = new Server(8000, new ControllerRouter());
            MvcEngine.Run(server);
        }
    }
}
