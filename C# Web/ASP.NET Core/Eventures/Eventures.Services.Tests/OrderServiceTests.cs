namespace Eventures.Services.Tests
{
    using Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderServiceTests : DataServiceTests
    {
        private readonly OrderService orderService;

        public OrderServiceTests()
        {
            this.orderService = new OrderService(dbContext, this.mapper);
        }

        [Fact]
        public async Task All_ReturnsAllOrdersInDatabase()
        {
            this.dbContext.Orders.Add(new Order());
            this.dbContext.Orders.Add(new Order());
            this.dbContext.Orders.Add(new Order());

            await dbContext.SaveChangesAsync();

            const int expectedCount = 3;
            var actualCount = this.orderService.All<FakeOrderViewModel>().Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task All_ReturnsCorrectTModelType()
        {
            dbContext.Orders.Add(new Order());

            await dbContext.SaveChangesAsync();

            var expectedType = typeof(FakeOrderViewModel);
            var actualType = this.orderService.All<FakeOrderViewModel>().First().GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public async Task All_ReturnsOrderedByOrderedOnDateAscending()
        {
            dbContext.Orders.Add(new Order { OrderedOn = new DateTime(2015, 3, 22, 22, 30, 55) });
            dbContext.Orders.Add(new Order { OrderedOn = new DateTime(2018, 3, 20, 2, 30, 00) });
            dbContext.Orders.Add(new Order { OrderedOn = new DateTime(2013, 8, 5, 10, 10, 45) });
            dbContext.Orders.Add(new Order { OrderedOn = new DateTime(2017, 3, 22, 22, 30, 55) });
            dbContext.Orders.Add(new Order { OrderedOn = new DateTime(2015, 3, 22, 22, 30, 56) });

            await dbContext.SaveChangesAsync();

            var expectedCollection = this.dbContext
                .Orders
                .OrderBy(e => e.OrderedOn)
                .ToList();

            var actualCollection = this.orderService.All<FakeOrderViewModel>().ToList();

            for (int i = 0; i < expectedCollection.Count; i++)
            {
                Assert.Equal(expectedCollection[i].Id, actualCollection[i].Id);
            }
        }

        [Fact]
        public async Task AllByUser_ReturnsAllOrdersInDatabaseByUserId_WhenUserIdExists()
        {
            var searchedId = Guid.NewGuid().ToString();

            var @event = new Event { Start = new DateTime(2018, 10, 12, 10, 10, 10) };
            this.dbContext.Orders.Add(new Order { CustomerId = searchedId, Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = searchedId, Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order{ Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = searchedId, Event = @event });

            await dbContext.SaveChangesAsync();

            const int expectedCount = 3;
            var actualCount = this.orderService.AllByUser<FakeOrderViewModel>(searchedId).Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task AllByUser_Returns0Orders_WhenUserIdDoesNotExists()
        {
            var searchedId = Guid.NewGuid().ToString();

            var @event = new Event { Start = new DateTime(2018, 10, 12, 10, 10, 10) };
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });
            this.dbContext.Orders.Add(new Order { Event = @event });
            this.dbContext.Orders.Add(new Order { CustomerId = Guid.NewGuid().ToString(), Event = @event });

            await dbContext.SaveChangesAsync();

            const int expectedCount = 0;
            var actualCount = this.orderService.AllByUser<FakeOrderViewModel>(searchedId).Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task AllByUser_ReturnsCorrectTModelType()
        {
            var searchedId = Guid.NewGuid().ToString();

            var @event = new Event { Start = new DateTime(2018, 10, 12, 10, 10, 10) };
            this.dbContext.Orders.Add(new Order { CustomerId = searchedId, Event = @event });

            await dbContext.SaveChangesAsync();

            var expectedType = typeof(FakeOrderViewModel);
            var actualType = this.orderService.AllByUser<FakeOrderViewModel>(searchedId).First().GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public async Task AllByUser_ReturnsOrderedByEventStartDateAscending()
        {
            var customerId = Guid.NewGuid().ToString();

            this.dbContext.Orders.Add(new Order { CustomerId = customerId, Event = new Event { Start = new DateTime(2018, 10, 12, 10, 10, 10) } });                                                                     
            this.dbContext.Orders.Add(new Order { CustomerId = customerId, Event = new Event { Start = new DateTime(2017, 10, 12, 3, 10, 10) } });                                                                      
            this.dbContext.Orders.Add(new Order { CustomerId = customerId, Event = new Event { Start = new DateTime(2018, 3, 12, 10, 10, 9) } });
            this.dbContext.Orders.Add(new Order { CustomerId = customerId, Event = new Event { Start = new DateTime(2015, 8, 22, 2, 10, 10) } });
            this.dbContext.Orders.Add(new Order { CustomerId = customerId, Event = new Event { Start = new DateTime(2018, 10, 12, 10, 55, 10) } });

            await dbContext.SaveChangesAsync();

            var expectedCollection = this.dbContext
                .Orders
                .OrderBy(e => e.Event.Start)
                .ToList();

            var actualCollection = this.orderService.AllByUser<FakeOrderViewModel>(customerId).ToList();

            for (int i = 0; i < expectedCollection.Count; i++)
            {
                Assert.Equal(expectedCollection[i].Id, actualCollection[i].Id);
            }
        }

        [Fact]
        public void Create_AddNewOrderToDb()
        {
            this.orderService.Create(1, Guid.NewGuid().ToString(), 1);
            const int expected = 1;
            var actual = this.dbContext.Orders.Count();

            Assert.Equal(expected, actual);
        }

        public class FakeOrderViewModel
        {
            public string Id { get; set; }
        }
    }
}
