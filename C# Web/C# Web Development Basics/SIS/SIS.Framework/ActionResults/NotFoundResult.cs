namespace SIS.Framework.ActionResults
{
    using Contracts;
    using HTTP.Enums;
    using HTTP.Responses.Contracts;
    using WebServer.Results;

    public class NotFoundResult : IActionResult
    {
        public IHttpResponse Invoke() 
            => new HtmlResult("<h1>The page or resource was not found.</h1>", HttpResponseStatusCode.NotFound);
    }
}
