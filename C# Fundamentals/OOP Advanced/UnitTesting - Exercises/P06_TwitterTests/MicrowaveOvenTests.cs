
using System;
using System.Linq;
using System.Reflection;

using Moq;

using NUnit.Framework;

using P06_Twitter.Contracts;
using P06_Twitter.Models;

namespace P06_TwitterTests
{
    public class MicrowaveOvenTests
    {
        private IClient _microwaveOven;
        private Mock<IWriter> _writer;
        private Mock<IServer> _server;

        private void SetUp()
        {
            _writer = new Mock<IWriter>();
            _server = new Mock<IServer>();

            _microwaveOven = new MicrowaveOven(_writer.Object, _server.Object);
        }

        private FieldInfo GetFieldInfo(Type fieldType)
        {
            var fieldInfo = _microwaveOven
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == fieldType);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field of type {fieldType} not found.");

            return fieldInfo;
        }

        [Test]
        public void ValidCtor()
        {
            SetUp();

            var writerClientFieldInfo = GetFieldInfo(typeof(IWriter));
            var serverClientFieldInfo = GetFieldInfo(typeof(IServer));

            var writerValue = writerClientFieldInfo.GetValue(_microwaveOven);
            var serverValue = serverClientFieldInfo.GetValue(_microwaveOven);

            Assert.That(writerValue, Is.Not.EqualTo(null));
            Assert.That(serverValue, Is.Not.EqualTo(null));
        }

        [Test]
        public void SendTweetToServerShoudWorkCorrectly()
        {
            SetUp();
            _server.Setup(s => s.Save(It.IsAny<string>()));

            _microwaveOven.SendTweetToServer("Tweet");

            _server.Verify(w => w.Save(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void WriteTweetShoudWorkCorrectly()
        {
            SetUp();
            _writer.Setup(w => w.WriteLine(It.IsAny<string>()));

            _microwaveOven.WriteTweet("Tweet");

            _writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once);
        }
    }
}
