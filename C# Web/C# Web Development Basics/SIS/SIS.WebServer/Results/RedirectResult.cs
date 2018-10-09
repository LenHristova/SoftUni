namespace SIS.WebServer.Results
{
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;

    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            : base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.Add(new HttpHeader(GlobalConstants.HeaderNames.Location, location));
        }
    }
}
