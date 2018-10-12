namespace SIS.WebServer.Routing
{
    using HTTP.Enums;
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Api.Contracts;
    using HTTP.Common;
    using HTTP.Responses;
    using Results;

    public class ServerRoutingTable : IHttpHandler
    {
        public ServerRoutingTable()
        {
            this.Routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
            };
        }

        public Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> Routes { get; }

        public IHttpResponse Handle(IHttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            var isResource = requestPath.Contains(".");
            if (isResource)
            {
                return this.TryGetResource(requestPath);
            }

            var isRegisteredRoute = this.Routes.ContainsKey(requestMethod) &&
                                    this.Routes[requestMethod].ContainsKey(requestPath);
            if (isRegisteredRoute)
            {
                return this.Routes[requestMethod][requestPath].Invoke(request);
            }

            return new HttpResponse(HttpResponseStatusCode.NotFound);
        }

        //TODO Find better place
        private IHttpResponse TryGetResource(string requestPath)
        {
            var appLocation = Assembly.GetExecutingAssembly().Location;
            var appDirectory = Directory.GetParent(appLocation).Parent?.Parent?.Parent;

            var indexOfPathAndFileSeparator = requestPath.LastIndexOf("/");
            var file = requestPath.Substring(indexOfPathAndFileSeparator + 1);

            var indexOfFileAndExtensionSeparator = file.LastIndexOf(".");
            var fileFolder = file.Substring(indexOfFileAndExtensionSeparator + 1);

            var resourcePath = $"{appDirectory}/{GlobalConstants.AppResourceFolderName}/{fileFolder}/{file}";

            var resourceExists = File.Exists(resourcePath);

            if (resourceExists)
            {
                var content = File.ReadAllBytes(resourcePath);

                return new InlineResourceResult(content, HttpResponseStatusCode.Ok);
            }
            else
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }
        }
    }
}
