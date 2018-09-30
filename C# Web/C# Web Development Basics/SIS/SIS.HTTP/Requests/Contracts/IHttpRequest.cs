namespace SIS.HTTP.Requests.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Headers.Contracts;

    public interface IHttpRequest
    {
        HttpRequestMethod Method { get; }

        string Path { get; }

        string Url { get; }

        IHttpHeaderCollection Headers { get; }

        IDictionary<string, object> FormData { get; }

        IDictionary<string, object> QueryData { get; }
    }
}
