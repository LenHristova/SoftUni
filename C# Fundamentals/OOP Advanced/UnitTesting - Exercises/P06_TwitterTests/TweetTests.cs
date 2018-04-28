using System;
using System.Linq;
using System.Reflection;

using Moq;

using NUnit.Framework;

using P06_Twitter.Contracts;
using P06_Twitter.Models;

namespace P06_TwitterTests
{
    public class TweetTests
    {
        private ITweet _tweet;

        private FieldInfo GetFieldInfo(Type fieldType)
        {
            var fieldInfo = _tweet
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == fieldType);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field of type {fieldType} not found.");

            return fieldInfo;
        }

        [Test]
        public void ValidCtor()
        {
            var client = new Mock<IClient>();
            _tweet = new Tweet(client.Object);

            var tweetClientFieldInfo = GetFieldInfo(typeof(IClient));
            var clientValue = tweetClientFieldInfo.GetValue(_tweet);

            Assert.That(clientValue, Is.Not.EqualTo(null));
        }

        [Test]
        public void RetrieveMessageShoudMakeClientWriteTweetAndSendTweetToServer()
        {
            var client = new Mock<IClient>();
            client.Setup(c => c.WriteTweet(It.IsAny<string>()));
            client.Setup(c => c.SendTweetToServer(It.IsAny<string>()));

            _tweet = new Tweet(client.Object);
            _tweet.RetrieveMessage("Tweet");

            client.Verify(c => c.WriteTweet(It.IsAny<string>()), Times.Once);
            client.Verify(c => c.SendTweetToServer(It.IsAny<string>()), Times.Once);
        }
    }
}
