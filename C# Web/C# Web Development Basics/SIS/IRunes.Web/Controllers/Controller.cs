namespace IRunes.Web.Controllers
{
    using System;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.HTTP.Sessions;
    using SIS.WebServer.Results;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        private const string HtmlExtension = ".html";
        private const string ViewsPath = "../../../Views";
        private const string SharedFolder = "Shared";
        private const string LayoutFile = "_Layout";
        private const string RenderContentCommand = "@RenderBody()";

        protected Controller(IHttpRequest httpRequest)
        {
            this.HttpRequest = httpRequest;

            this.ViewBag = new Dictionary<string, string>
            {
                ["showError"] = "hidden",
                ["authDisplay"] = this.IsAuthenticated ? string.Empty : "hidden",
                ["notAuthDisplay"] = !this.IsAuthenticated ? string.Empty : "hidden",
            };
        }

        protected IHttpRequest HttpRequest { get; }

        protected bool IsAuthenticated => this.User != null;

        protected string User =>
            this.HttpRequest.Session.GetParameter(HttpSessionStorage.UsernameKey)?.ToString();

        protected void SignInUser(string username)
        {
            this.HttpRequest.Session.AddParameter(HttpSessionStorage.UsernameKey, username);
        }

        protected IDictionary<string, string> ViewBag { get; }

        protected IHttpResponse View([CallerMemberName]string viewName = "")
        {
            var layoutPath = $"{ViewsPath}/{SharedFolder}/{LayoutFile}{HtmlExtension}";
            var layout = string.Empty;

            var folderName = this.GetType().Name.Replace(nameof(Controller), "");
            var viewPath = $"{ViewsPath}/{folderName}/{viewName}{HtmlExtension}";
            var content = string.Empty;

            try
            {
                layout = File.ReadAllText(layoutPath);
                content = File.ReadAllText(viewPath);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFoundError();
            }

            var htmlContent = layout.Replace(RenderContentCommand, content);

            if (this.ViewBag.Any())
            {
                foreach (var value in this.ViewBag)
                {
                    htmlContent = htmlContent.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new HtmlResult(htmlContent, HttpResponseStatusCode.Ok);
        }

        protected void AddErrorMessageToViewData(string errorMessage)
        {
            this.ViewBag["error"] = errorMessage;
            this.ViewBag["showError"] = string.Empty;
        }

        protected IHttpResponse ServerError(string message)
        => new HtmlResult($"<h1>{message}</h1>", HttpResponseStatusCode.InternalServerError);

        protected IHttpResponse NotFoundError()
        => new HtmlResult("<h1>The page or resource was not found.</h1>", HttpResponseStatusCode.NotFound);

        protected bool IsValid(object obj, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
        }
    }
}
