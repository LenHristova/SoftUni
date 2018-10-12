namespace SIS.Framework.ActionResults
{
    using Contracts;
    using HTTP.Common;
    using HTTP.Responses.Contracts;

    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; }

        public IHttpResponse Invoke() 
            => new WebServer.Results.RedirectResult(this.RedirectUrl);
    }
}
