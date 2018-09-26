namespace P03_RequestParser
{
    using System;

    public class HttpResponse
    {
        public HttpResponse(StatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public StatusCode StatusCode { get; }

        public override string ToString()
            => $"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}{Environment.NewLine}" +
               $"Content-Length: {this.StatusCode.ToString().Length}{Environment.NewLine}" +
               $"Content-Type: text/plain{Environment.NewLine}" +
               $"{Environment.NewLine}" +
               $"{this.StatusCode}";

    }
}
