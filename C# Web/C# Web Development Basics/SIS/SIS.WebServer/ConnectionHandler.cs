namespace SIS.WebServer
{
    using Api.Contracts;
    using HTTP.Common;
    using HTTP.Cookies;
    using HTTP.Enums;
    using HTTP.Exceptions;
    using HTTP.Requests;
    using HTTP.Requests.Contracts;
    using HTTP.Responses.Contracts;
    using HTTP.Sessions;
    using Results;
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IHttpHandler handler;

        public ConnectionHandler(Socket client, IHttpHandler handler)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(handler, nameof(handler));

            this.client = client;
            this.handler = handler;
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequest();

                if (httpRequest != null)
                {
                    var sessionId = this.SetRequestSession(httpRequest);

                    var httpResponse = this.handler.Handle(httpRequest);

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

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}
