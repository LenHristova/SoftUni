namespace SIS.WebServer.Results
{
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;
    using System.Text;
    using HTTP.Common;

    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode statusCode)
        : base(statusCode)
        {
            this.Headers.Add(new HttpHeader(GlobalConstants.HeaderNames.ContentType, "text/plain; charset=utf-8"));

            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
