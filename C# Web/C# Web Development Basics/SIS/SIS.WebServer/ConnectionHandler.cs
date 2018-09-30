namespace SIS.WebServer
{
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Requests;
    using HTTP.Requests.Contracts;
    using HTTP.Responses;
    using HTTP.Responses.Contracts;
    using Routing;
    using System;
    using System.Net.Sockets;
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
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpResponse = this.HandleRequest(httpRequest);
                await this.PrepareResponse(httpResponse);
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

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            var routes = this.serverRoutingTable.Routes;
            var requestMethod = httpRequest.Method;
            var requestPath = httpRequest.Path;

            if (!routes.ContainsKey(requestMethod) ||
                !routes[requestMethod].ContainsKey(requestPath))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            return routes[requestMethod][requestPath].Invoke(httpRequest);
        }

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}
