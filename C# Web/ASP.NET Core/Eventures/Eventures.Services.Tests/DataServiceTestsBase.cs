namespace Eventures.Services.Tests
{
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Web.Mapping;

    //xUnit creates a new instance of the test class for every test that is run,
    //so any code which is placed into the constructor of the test class will be run for every single test.
    //This makes the constructor a convenient place to put reusable context setup code
    //where to share the code without sharing object instances
    //=> get a clean copy of the context object(s) for every test that is run.
    public class DataServiceTests : IDisposable //shared setup/cleanup code without sharing object instances
    {
        protected readonly EventuresDbContext dbContext;
        protected IMapper mapper;

        //set test context
        public DataServiceTests()
        {
            this.dbContext = new EventuresDbContext(this.GetDbOptions());
            this.SetMapper();
        }

        //clean test context
        public void Dispose() => this.dbContext.Dispose();

        private DbContextOptions<EventuresDbContext> GetDbOptions()
            => new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                .Options;

        private void SetMapper()
        {
            if (this.mapper == null)
            {
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile<EventuresProfile>());
                this.mapper = new Mapper(configuration);
            }
        }
    }
}
