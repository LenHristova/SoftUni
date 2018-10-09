namespace SIS.HTTP.Requests.Contracts
{
    using System.Collections.Generic;
    using Cookies.Contracts;
    using Enums;
    using Headers.Contracts;
    using Sessions.Contracts;

    public interface IHttpRequest
    {
        HttpRequestMethod Method { get; }

        string Path { get; }

        string Url { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        IHttpSession Session { get; set; }

        IDictionary<string, object> FormData { get; }

        IDictionary<string, object> QueryData { get; }
    }
}
