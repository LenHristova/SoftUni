namespace SIS.HTTP.Responses
{
    using Common;
    using Contracts;
    using Cookies;
    using Cookies.Contracts;
    using Enums;
    using Extensions;
    using Headers;
    using Headers.Contracts;
    using System.Linq;
    using System.Text;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() { }

        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
            => this.Headers.Add(header);

        public void AddCookie(HttpCookie cookie)
            => this.Cookies.Add(cookie);

        public byte[] GetBytes()
            => Encoding.UTF8
                .GetBytes(this.ToString())
                .Concat(this.Content)
                .ToArray();

        public override string ToString()
        {
            var result = new StringBuilder();

            result
                .AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseLine()}")
                .AppendLine(this.Headers.ToString());

            if (this.Cookies.HasCookies())
            {
                foreach (var cookie in this.Cookies)
                {
                    result
                        .AppendLine($"{GlobalConstants.HeaderNames.SetCookie}: {cookie}");
                }
            }

            result.AppendLine();

            return result.ToString();
        }
    }
}
