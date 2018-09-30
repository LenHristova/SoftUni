namespace SIS.HTTP.Requests
{
    using Common;
    using Contracts;
    using Enums;
    using Exceptions;
    using Headers;
    using Headers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public HttpRequestMethod Method { get; private set; }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public IHttpHeaderCollection Headers { get; }

        public IDictionary<string, object> FormData { get; }

        public IDictionary<string, object> QueryData { get; }

        private void ParseRequest(string requestString)
        {
            //Split request by lines
            var requestAllLines = requestString.Split(Environment.NewLine);
            //Get index of blank line separator
            var indexOfEmptyLine = Array.IndexOf(requestAllLines, string.Empty);

            if (!this.IsValidRequestLines(requestAllLines, indexOfEmptyLine))
            {
                throw new BadRequestException();
            }

            //Process Request Line - parse method, url and path
            var requestLine = requestAllLines[0].Trim();
            this.ParseRequestLine(requestLine);

            //Process Request Headers
            var headerLines = requestAllLines.Skip(1).Take(indexOfEmptyLine - 1).ToArray();
            this.ParseHeaders(headerLines);

            //Process Request Message Body
            var bodyContent = requestAllLines.Skip(indexOfEmptyLine + 1).FirstOrDefault();
            this.ParseParameters(bodyContent);
        }

        // Request must have:
        // - at least 3 lines (Request Line; at least 1 Header; Empty Line; Message Body (optional))
        // - empty line after headers
        private bool IsValidRequestLines(string[] requestAllLines, int indexOfEmptyLine)
        => requestAllLines.Length >= 3 &&
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
            => requestLineParts.Length == 3 &&
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
            var path = this.Url.Split('?', '#').FirstOrDefault();

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
                var headerArgs = headerLine.Split(": ", StringSplitOptions.RemoveEmptyEntries);

                if (headerArgs.Length != 2)
                {
                    throw new BadRequestException();
                }

                var key = headerArgs[0];
                var value = headerArgs[1];

                var header = new HttpHeader(key, value);

                this.Headers.Add(header);
            }

            var hasHost = this.Headers.Contains(HttpHeader.Host);
            if (!hasHost)
            {
                throw new BadRequestException();
            }
        }

        private void ParseParameters(string bodyContent)
        {
            var urlContainsParameters = this.Url.Contains("?");
            if (urlContainsParameters)
            {
                var queryString = this.Url.Split('?', '#')[1];
                this.ExtractParameters(queryString, this.QueryData);
            }

            var requestHasBody = !string.IsNullOrEmpty(bodyContent);
            if (requestHasBody)
            {
                this.ExtractParameters(bodyContent, this.FormData);
            }
        }

        private void ExtractParameters(
            string queryString, 
            IDictionary<string, object> parametersCollection)
        {
            if (!this.IsValidQueryString(queryString))
            {
                throw new BadRequestException();
            }

            var pairs = queryString.Split("&");
            foreach (var pair in pairs)
            {
                var args = pair.Split("=");
                if (args.Length != 2)
                {
                    throw new BadRequestException();
                }

                var key = WebUtility.UrlDecode(args[0]);
                var value = WebUtility.UrlDecode(args[1]);

                parametersCollection[key] = value;
            }
        }

        private bool IsValidQueryString(string queryString)
            => !string.IsNullOrEmpty(queryString) &&
               queryString.Contains("=");
    }
}
