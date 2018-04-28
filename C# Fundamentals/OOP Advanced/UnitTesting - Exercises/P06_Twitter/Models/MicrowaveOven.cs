using P06_Twitter.Contracts;

namespace P06_Twitter.Models
{
    public class MicrowaveOven : IClient
    {
        private readonly IWriter _writer;
        private readonly IServer _server;

        public MicrowaveOven(IWriter writer, IServer server)
        {
            _writer = writer;
            _server = server;
        }

        public void SendTweetToServer(string message) => _server.Save(message);

        public void WriteTweet(string message) => _writer.WriteLine(message);
    }
}
