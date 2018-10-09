namespace SIS.HTTP.Headers
{
    using Common;
    using Contracts;
    using System;
    using System.Collections.Generic;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            CoreValidator.ThrowIfNullOrEmpty(header.Key, nameof(header.Key));
            CoreValidator.ThrowIfNullOrEmpty(header.Value, nameof(header.Value));

            if (this.ContainsKey(header.Key))
            {
                throw new InvalidOperationException(
                    $"HttpHeaderCollection already contains key \"{header.Key}\"");
            }

            this.headers.Add(header.Key, header);
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
            => this.ContainsKey(key) 
                ? this.headers[key] 
                : null;

        public override string ToString() =>
            string.Join(Environment.NewLine, this.headers.Values);
    }
}
