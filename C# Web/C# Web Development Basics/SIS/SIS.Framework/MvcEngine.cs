namespace SIS.Framework
{
    using System;
    using System.Reflection;
    using WebServer;

    public static class MvcEngine
    {
        public static void Run(Server server)
        {
            ConfigureMvcContext(MvcContext.Instance);

            try
            {
                server.Run();
            }
            catch (Exception e)
            {
                //Log errors
                Console.WriteLine(e.InnerException.Message);
            }
        }

        private static void ConfigureMvcContext(MvcContext context)
        {
            context.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;
            context.ControllersSuffix = "Controller";
            context.ControllersFolder = "Controllers";
            context.ViewsFolder = "Views";
            context.ModelsFolder = "Models";
            context.ResourcesFolder = "Resources";
        }
    }
}
