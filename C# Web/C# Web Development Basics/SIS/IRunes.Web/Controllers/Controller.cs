namespace IRunes.Web.Controllers
{
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
        private const string LayoutPath = ViewsPath + "/Shared/_Layout" + HtmlExtension;
        private const string ContentPlaceholder = "{{{content}}}";

        protected Controller(IHttpRequest httpRequest)
        {
            this.HttpRequest = httpRequest;

            this.ViewBag = new Dictionary<string, string>
            {
                ["showError"] = "none",
                ["authDisplay"] = this.IsAuthenticated ? "block" : "none",
                ["notAuthDisplay"] = !this.IsAuthenticated ? "block" : "none",
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
            var folderName = this.GetType().Name
                .Replace(nameof(Controller), "");

            var viewPath = $"{ViewsPath}/{folderName}/{viewName}{HtmlExtension}";

            if (!File.Exists(LayoutPath))
            {
                return this.ServerError($"View {LayoutPath} was not found.");
            }

            if (!File.Exists(viewPath))
            {
                return this.ServerError($"View {viewPath} was not found.");
            }

            var layout = File.ReadAllText(LayoutPath);
            var content = File.ReadAllText(viewPath);

            content = layout.Replace(ContentPlaceholder, content);

            if (this.ViewBag.Any())
            {
                foreach (var value in this.ViewBag)
                {
                    content = content.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        protected void AddErrorMessageToViewData(string errorMessage)
        {
            this.ViewBag["error"] = errorMessage;
            this.ViewBag["showError"] = "block";
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
