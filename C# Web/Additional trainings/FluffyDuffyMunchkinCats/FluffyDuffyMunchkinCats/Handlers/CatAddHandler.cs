namespace FluffyDuffyMunchkinCats.Handlers
{
	using System;
	using Data;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

    public class CatAddHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition => 
            context => context.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler
            => async (context) =>
            {
                if (context.Request.Method == HttpMethods.Get)
                {
                    await context.Response
                        .WriteAsync($@"<h1>Add cat</h1>
                                       <a href=""/"">Back to home</a>
                                       <form method=""post"">
                                       <label for=""Name"">Name: </label>
                                       <input type=""text"" name=""Name"" id=""Name"" /><br/>
                                       <label for=""Age"">Age: </label>
                                       <input type=""number"" name=""Age"" id=""Age"" /><br/>
                                       <label for=""Breed"">Breed: </label>
                                       <input type=""text"" name=""Breed"" id=""Breed"" /><br/>
                                       <label for=""ImageUrl"">Image Url: </label>
                                       <input type=""link"" name=""ImageUrl"" id=""ImageUrl"" /><br/>
                                       <input type=""submit"" value=""Add Cat"" />
                                       </form>");
                }
                else if (context.Request.Method == HttpMethods.Post)
                {
                    var formData = context.Request.Form;

                    var name = formData["Name"];
                    var validAge = int.TryParse(formData["Age"], out var age);
                    var breed = formData["Breed"];
                    var imageUrl = formData["ImageUrl"];

                    try
                    {
                        if (string.IsNullOrWhiteSpace(name) ||
                            string.IsNullOrWhiteSpace(breed) ||
                            string.IsNullOrWhiteSpace(imageUrl) ||
                            !validAge)
                        {
                            throw new InvalidOperationException("Ivalid cat data!");
                        }

                        var cat = new Cat
                        {
                            Name = name,
                            Age = age,
                            Breed = breed,
                            ImageUrl = imageUrl,
                        };

                        var db = context.RequestServices.GetRequiredService<CatsContext>();

                        using (db)
                        {
                            db.Cats.Add(cat);
                            await db.SaveChangesAsync();
                        }

                        context.Response.Redirect("/");
                    }
                    catch
                    {
                        await context.Response
                            .WriteAsync(@"<h2>Ivalid cat data!</h2>
                                          <a href=""/cat/add"">Back to the form</a>");
                    }
                }
            };
    }
}
