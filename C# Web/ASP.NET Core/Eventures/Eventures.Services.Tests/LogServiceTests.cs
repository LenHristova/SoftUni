namespace Eventures.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class LogServiceTests : DataServiceTests
    {
        private readonly LogService logService;

        public LogServiceTests()
        {
            this.logService = new LogService(this.dbContext);
        }

        [Fact]
        public void Create_AddNewOrderToDb()
        {
            this.logService.Log("Log", new EventId(), LogLevel.Error);
            const int expected = 1;
            var actual = this.dbContext.Logs.Count();

            Assert.Equal(expected, actual);
        }
    }
}
