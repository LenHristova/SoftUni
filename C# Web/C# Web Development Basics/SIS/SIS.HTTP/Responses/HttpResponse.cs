namespace SIS.HTTP.Responses
{
    using Contracts;
    using Enums;
    using Headers;
    using Headers.Contracts;
    using System;
    using System.Linq;
    using System.Text;
    using Common;
    using Extensions;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() { }

        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
            => this.Headers.Add(header);

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
                .AppendLine(this.Headers.ToString())
                .AppendLine();

            return result.ToString();
        }
    }
}
