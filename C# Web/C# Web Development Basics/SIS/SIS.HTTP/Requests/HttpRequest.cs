namespace SIS.HTTP.Requests
{
    using Common;
    using Contracts;
    using Cookies;
    using Cookies.Contracts;
    using Enums;
    using Exceptions;
    using Headers;
    using Headers.Contracts;
    using Sessions.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        private const char UrlQuerySeparator = '?';
        private const char UrlFragmentSeparator = '#';
        private const string HeaderNameValueSeparator = ": ";
        private const char ParameterSeparator = '&';
        private const char ParameterNameValueSeparator = '=';
        private const char CookieNameValueSeparator = '=';
        private const string CookieHeaderValueNameValueSeparator = "; ";
        private const int MinRequestLinesCount = 3;
        private const int RequestLinePartsCount = 3;
        private const int KeyValuePartsCount = 2;

        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public HttpRequestMethod Method { get; private set; }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public IHttpSession Session { get; set; }

        public IDictionary<string, object> FormData { get; }

        public IDictionary<string, object> QueryData { get; }

        private void ParseRequest(string requestString)
        {
            //Split request by lines
            var requestAllLines = requestString.Split(Environment.NewLine);
            //Get index of empty line separator
            var indexOfEmptyLine = Array.IndexOf(requestAllLines, string.Empty);

            if (!this.IsValidRequestLines(requestAllLines.Length, indexOfEmptyLine))
            {
                throw new BadRequestException();
            }

            //Process Request Line - parse method, url and path
            var requestLine = requestAllLines[0].Trim();
            this.ParseRequestLine(requestLine);

            //Process Request Headers
            var headerLines = requestAllLines.Skip(1).Take(indexOfEmptyLine - 1).ToArray();
            this.ParseHeaders(headerLines);

            //Process Cookies
            this.ParseCookies();

            //Process Request Message Body
            var bodyContent = requestAllLines.Skip(indexOfEmptyLine + 1).FirstOrDefault();
            this.ParseParameters(bodyContent);
        }

        // Request must have:
        // - at least 3 lines (Request Line; at least 1 Header; Empty Line; Message Body (optional))
        // - empty line after header(s)
        private bool IsValidRequestLines(int requestLinesCount, int indexOfEmptyLine)
        => requestLinesCount >= MinRequestLinesCount &&
           indexOfEmptyLine > 1;

        private void ParseRequestLine(string requestLine)
        {
            var requestLineParts = requestLine
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParts))
            {
                throw new BadRequestException();
            }

            this.ParseMethod(requestLineParts[0]);
            this.ParseUrl(requestLineParts[1]);
            this.ParsePath();
        }

        // Request Line must have:
        // - 3 parts (method, url, protocol)
        // - valid protocol
        private bool IsValidRequestLine(string[] requestLineParts)
            => requestLineParts.Length == RequestLinePartsCount &&
               requestLineParts[2].ToUpper() == GlobalConstants.HttpOneProtocolFragment;

        private void ParseMethod(string methodString)
        {
            var isValidMethod = Enum.TryParse<HttpRequestMethod>(methodString, true, out var method);
            if (!isValidMethod)
            {
                throw new BadRequestException();
            }

            this.Method = method;
        }

        private void ParseUrl(string urlString)
        {
            var isValidUrl = !string.IsNullOrEmpty(urlString);

            if (!isValidUrl)
            {
                throw new BadRequestException();
            }

            this.Url = urlString;
        }

        private void ParsePath()
        {
            var path = this.Url
                .Split(UrlQuerySeparator, UrlFragmentSeparator)
                .FirstOrDefault();

            var isValidPath = !string.IsNullOrEmpty(path);
            if (!isValidPath)
            {
                throw new BadRequestException();
            }

            this.Path = path;
        }

        private void ParseHeaders(string[] headerLines)
        {
            if (!headerLines.Any())
            {
                throw new BadRequestException();
            }

            foreach (var headerLine in headerLines)
            {
                var headerKvp = headerLine
                    .Split(HeaderNameValueSeparator, StringSplitOptions.RemoveEmptyEntries);

                if (headerKvp.Length != KeyValuePartsCount)
                {
                    throw new BadRequestException();
                }

                var key = headerKvp[0];
                var value = headerKvp[1];

                var header = new HttpHeader(key, value);

                this.Headers.Add(header);
            }

            var hasHost = this.Headers.ContainsKey(GlobalConstants.HeaderNames.Host);
            if (!hasHost)
            {
                throw new BadRequestException();
            }
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsKey(GlobalConstants.HeaderNames.Cookie))
            {
                var cookies = this.Headers.GetHeader(GlobalConstants.HeaderNames.Cookie).Value
                    .Split(CookieHeaderValueNameValueSeparator, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    var cookieKvp = cookie
                        .Split(CookieNameValueSeparator, KeyValuePartsCount);

                    if (cookieKvp.Length != KeyValuePartsCount)
                    {
                        throw new BadRequestException();
                    }

                    var key = cookieKvp[0];
                    var value = cookieKvp[1];

                    this.Cookies.Add(new HttpCookie(key, value, false));
                }
            }
        }

        private void ParseParameters(string bodyContent)
        {
            var urlContainsParameters = this.Url.Contains(UrlQuerySeparator);
            if (urlContainsParameters)
            {
                var queryString = this.Url.Split(UrlQuerySeparator, UrlFragmentSeparator)[1];

                this.ExtractParameters(queryString, this.QueryData);
            }

            this.ExtractParameters(bodyContent, this.FormData);
        }

        private void ExtractParameters(
            string parametersString,
            IDictionary<string, object> parametersCollection)
        {
            if (string.IsNullOrWhiteSpace(parametersString))
            {
                return;
            }

            var parameters = parametersString.Split(ParameterSeparator);
            foreach (var parameter in parameters)
            {
                var parameterKvp = parameter.Split(ParameterNameValueSeparator);

                if (!this.IsValidParameter(parameterKvp))
                {
                    throw new BadRequestException();
                }

                var key = parameterKvp[0];
                var value = WebUtility.UrlDecode(parameterKvp[1]);

                if (parametersCollection.ContainsKey(key))
                {
                    throw new BadRequestException();
                }

                value = string.IsNullOrEmpty(value) ? null : value;

                parametersCollection.Add(key, value);
            }
        }

        private bool IsValidParameter(string[] parameterKvp)
            => parameterKvp.Length == KeyValuePartsCount &&
               !string.IsNullOrWhiteSpace(parameterKvp[0]);
    }
}
