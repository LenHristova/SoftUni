namespace SIS.WebServer
{
    using HTTP.Common;
    using HTTP.Cookies;
    using HTTP.Enums;
    using HTTP.Exceptions;
    using HTTP.Requests;
    using HTTP.Requests.Contracts;
    using HTTP.Responses;
    using HTTP.Responses.Contracts;
    using HTTP.Sessions;
    using Results;
    using Routing;
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly ServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, ServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequest();

                if (httpRequest != null)
                {
                    var sessionId = this.SetRequestSession(httpRequest);

                    var httpResponse = this.HandleRequest(httpRequest);

                    this.SetResponseSession(httpResponse, sessionId);

                    await this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                var readBytes = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (readBytes == 0)
                {
                    break;
                }

                var currentBytesToString = Encoding.UTF8.GetString(data.Array, 0, readBytes);

                result.Append(currentBytesToString);

                if (readBytes < 1023)
                {
                    break;
                }
            }

            return result.Length > 0
                ? new HttpRequest(result.ToString())
                : null;
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            var sessionCookieKey = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);

            var sessionId = sessionCookieKey?.Value ?? Guid.NewGuid().ToString();

            httpRequest.Session = HttpSessionStorage.GetSession(sessionId);

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                var sessionCookie = new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId);

                httpResponse.AddCookie(sessionCookie);
            }
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            var routes = this.serverRoutingTable.Routes;
            var requestMethod = httpRequest.Method;
            var requestPath = httpRequest.Path;

            var isResource = requestPath.Contains(".");
            if (isResource)
            {
                return this.TryGetResource(requestPath);
            }

            var isRegisteredRoute = routes.ContainsKey(requestMethod) &&
                                    routes[requestMethod].ContainsKey(requestPath);
            if (isRegisteredRoute)
            {
                return routes[requestMethod][requestPath].Invoke(httpRequest);
            }

            return new HttpResponse(HttpResponseStatusCode.NotFound);
        }

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

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}
