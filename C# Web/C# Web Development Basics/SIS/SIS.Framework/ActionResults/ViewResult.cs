namespace SIS.Framework.ActionResults
{
    using Contracts;
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Responses.Contracts;
    using WebServer.Results;

    public class ViewResult : IViewable
    {
        public ViewResult(IRenderable view)
        {
            CoreValidator.ThrowIfNull(view, nameof(view));

            this.View = view;
        }

        public IRenderable View { get; }

        public IHttpResponse Invoke() 
            => new HtmlResult(this.View.Render(), HttpResponseStatusCode.Ok);
    }
}
