namespace SIS.Framework.Controllers
{
    using ActionResults.Contracts;
    using HTTP.Requests.Contracts;
    using System.Runtime.CompilerServices;
    using ActionResults;
    using Views;

    public abstract class Controller
    {
        public IHttpRequest Request { get; set; }

        protected IViewable View([CallerMemberName] string viewName = "")
        {
            var viewPath = this.GetViewPath(this.Name, viewName);

            return new ViewResult(new View(viewPath));
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        => new RedirectResult(redirectUrl);

        private string Name => this.GetType().Name
                .Replace(MvcContext.Instance.ControllersSuffix, string.Empty);

        private string GetViewPath(string controller, string action)
            => string.Format("../../../{0}/{1}/{2}",
                MvcContext.Instance.ViewsFolder,
                controller,
                action);
    }
}
