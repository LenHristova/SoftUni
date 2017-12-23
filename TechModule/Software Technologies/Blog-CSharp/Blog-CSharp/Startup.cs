
using System.Data.Entity;
using Blog_CSharp.Migrations;
using Blog_CSharp.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog_CSharp.Startup))]
namespace Blog_CSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}