using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CameraBazar.Web.Areas.Identity.IdentityHostingStartup))]
namespace CameraBazar.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}