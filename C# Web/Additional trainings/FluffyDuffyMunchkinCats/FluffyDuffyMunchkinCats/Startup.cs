namespace FluffyDuffyMunchkinCats
{
    using Data;
    using Infrastucture.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<CatsContext>(options =>
                options.UseSqlServer("Server=LEN\\SQLEXPRESS;Database=Cats;Integrated Security=True;"));        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDatabaseMigration()
                .UseHtmlContentType()
                .UseRequestHandlers()
                .UseNotFoundHandler();
        }
    }
}
