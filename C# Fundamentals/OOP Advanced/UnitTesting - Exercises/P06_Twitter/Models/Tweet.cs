using P06_Twitter.Contracts;

namespace P06_Twitter.Models
{
    public class Tweet : ITweet
    {
        private readonly IClient _client;

        public Tweet(IClient client)
        {
            _client = client;
        }

        public void RetrieveMessage(string message)
        {
            _client.WriteTweet(message);
            _client.SendTweetToServer(message);
        }
    }
}
