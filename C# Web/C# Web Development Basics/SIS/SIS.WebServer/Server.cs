﻿namespace SIS.WebServer
{
    using Api.Contracts;
    using HTTP.Common;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";
        private readonly int port;
        private readonly TcpListener listener;
        private readonly IHttpHandler handle;
        private bool isRunning;

        public Server(int port, IHttpHandler handle)
        {
            CoreValidator.ThrowIfNull(handle, nameof(handle));

            this.port = port;
            this.listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);
            this.handle = handle;
        }

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started at http://{LocalhostIpAddress}:{port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");

                var client = listener.AcceptSocketAsync().GetAwaiter().GetResult();

                Task.Run(() => Listen(client));
            }
        }

        public async void Listen(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, this.handle);
            await connectionHandler.ProcessRequestAsync();
        }
    }
}
