namespace FluffyDuffyMunchkinCats.Handlers
{
	using System;
	using Data;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

    public class CatDetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition
            => context => context.Request.Path.Value.StartsWith("/cat") &&
                          context.Request.Method == HttpMethods.Get;

        public RequestDelegate RequestHandler
            => async (context) =>
            {
                var pathParts = context.Request.Path.Value
                    .Split("/", StringSplitOptions.RemoveEmptyEntries);

                if (pathParts.Length < 2)
                {
                    context.Response.Redirect("/");
                    return;
                }

                var catId = int.TryParse(pathParts[1], out var id) ? id : (int?) null;

                var db = context.RequestServices.GetRequiredService<CatsContext>();

                using (db)
                {
                    var cat = await db.Cats.FindAsync(catId);

                    if (cat == null)
                    {
                        context.Response.Redirect("/");
                        return;
                    }

                    await context.Response
                        .WriteAsync($@"<h1>{cat.Name}</h1>
                                       <a href=""/"">Back to home</a><br/>
                                       <img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>
                                       <p>Age: {cat.Age}</p>
                                       <p>Breed: {cat.Breed}</p>");
                }

            };
    }
}
