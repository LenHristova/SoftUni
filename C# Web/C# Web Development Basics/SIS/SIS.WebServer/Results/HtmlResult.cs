namespace SIS.WebServer.Results
{
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;
    using System.Text;

    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode statusCode)
            : base(statusCode)
        {
            this.Headers.Add(new HttpHeader(GlobalConstants.HeaderNames.ContentType, "text/html; charset=utf-8"));

            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
