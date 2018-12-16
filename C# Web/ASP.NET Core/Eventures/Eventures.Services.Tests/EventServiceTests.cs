namespace Eventures.Services.Tests
{
    using Common.Models.Events;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Web.Mapping;
    using Xunit;

    public class EventServiceTests : DataServiceTests
    {
        private readonly EventService eventService;

        //set test context
        public EventServiceTests()
        {
            this.eventService = new EventService(this.dbContext, this.mapper);
        }

        [Fact]
        public async Task All_ReturnsAllEvents_ThatEndDateDoesNotPassedAndTotalTicketsCountIsGreaterThen0()
        {
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddHours(1), TotalTickets = 1 }); //Valid
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddHours(-1), TotalTickets = 1 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(1), TotalTickets = 30 }); //Valid
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(1), TotalTickets = 0 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(1), TotalTickets = -1 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(100), TotalTickets = 100 }); //Valid

            await dbContext.SaveChangesAsync();

            const int expectedCount = 3;
            var actualCount = this.eventService.All<FakeEventViewModel>().Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task All_Returns0Events_WhenAllEventsEndDateIsPassedAndTotalTicketsCountIsGreaterThen0()
        {
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddHours(-1), TotalTickets = 1 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(-1), TotalTickets = 30 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(-100), TotalTickets = 100 });

            await dbContext.SaveChangesAsync();

            const int expectedCount = 0;
            var actualCount = this.eventService.All<FakeEventViewModel>().Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task All_Returns0Events_WhenEndDateDoesNotPassedAndTotalTicketsCountIs0OrLessThen0()
        {
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddHours(1), TotalTickets = 0 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(1), TotalTickets = 0 });
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddDays(100), TotalTickets = -1 });

            await dbContext.SaveChangesAsync();

            const int expectedCount = 0;
            var actualCount = this.eventService.All<FakeEventViewModel>().Count();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task All_ReturnsCorrectTModelType()
        {
            dbContext.Events.Add(new Event { End = DateTime.UtcNow.AddHours(1), TotalTickets = 1 });

            await dbContext.SaveChangesAsync();

            var expectedType = typeof(FakeEventViewModel);
            var actualType = this.eventService.All<FakeEventViewModel>().First().GetType();

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public async Task All_ReturnsOrderedByStartDateAscending()
        {
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(1), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddHours(1), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(30), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(10), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddHours(3), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(100), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(1), End = DateTime.MaxValue, TotalTickets = 1 });
            dbContext.Events.Add(new Event { Start = DateTime.UtcNow.AddDays(15), End = DateTime.MaxValue, TotalTickets = 1 });

            await dbContext.SaveChangesAsync();

            var expectedCollection = this.dbContext
                .Events
                .OrderBy(e => e.Start)
                .ToList();

            var actualCollection = this.eventService.All<FakeEventViewModel>().ToList();

            for (int i = 0; i < expectedCollection.Count; i++)
            {
                Assert.Equal(expectedCollection[i].Id, actualCollection[i].Id);
            }
        }

        [Fact]
        public void Create_AddNewEventToDb()
        {
            this.eventService.Create(new CreateEventInputModel());
            const int expected = 1;
            var actual = this.dbContext.Events.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task IsAvailable_ReturnsTrue_WhenGivenIdExistsAndEndDateDoesNotPassedAndTotalTicketsCountIsGreaterThen0()
        {
            const int idToSearch = 1;
            this.dbContext.Events.Add(new Event { Id = idToSearch, End = DateTime.UtcNow.AddHours(1), TotalTickets = 1 });
            await dbContext.SaveChangesAsync();

            Assert.True(this.eventService.IsAvailable(idToSearch));
        }

        [Fact]
        public async Task IsAvailable_ReturnsFalse_WhenGivenIdDoesNotExists()
        {
            this.dbContext.Events.Add(new Event { Id = 1, End = DateTime.UtcNow.AddDays(1), TotalTickets = 1 });
            this.dbContext.Events.Add(new Event { Id = 2, End = DateTime.UtcNow.AddDays(1), TotalTickets = 1 });
            this.dbContext.Events.Add(new Event { Id = 3, End = DateTime.UtcNow.AddDays(1), TotalTickets = 1 });
            await dbContext.SaveChangesAsync();

            const int idToSearch = 4;
            Assert.False(this.eventService.IsAvailable(idToSearch));
        }

        [Fact]
        public async Task IsAvailable_ReturnsFalse_WhenEndDateIsPassed()
        {
            const int idToSearch = 1;
            this.dbContext.Events.Add(new Event { Id = idToSearch, End = DateTime.UtcNow.AddDays(-1), TotalTickets = 1 });
            await dbContext.SaveChangesAsync();

            Assert.False(this.eventService.IsAvailable(idToSearch));
        }

        [Fact]
        public async Task IsAvailable_ReturnsFalse_WhenTotalTicketsCountIs0OrLessThen0()
        {
            var idsToSearch = new List<int> { 1, 2, 3 };

            this.dbContext.Events.Add(new Event { Id = idsToSearch[0], End = DateTime.UtcNow.AddDays(1), TotalTickets = 0 });
            this.dbContext.Events.Add(new Event { Id = idsToSearch[1], End = DateTime.UtcNow.AddDays(1), TotalTickets = -1 });
            this.dbContext.Events.Add(new Event { Id = idsToSearch[2], End = DateTime.UtcNow.AddDays(1), TotalTickets = -100 });
            await dbContext.SaveChangesAsync();

            foreach (var idToSearch in idsToSearch)
            {
                Assert.False(this.eventService.IsAvailable(idToSearch));
            }
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(5, 30)]
        [InlineData(10, 300)]
        [InlineData(10, 0)]
        [InlineData(100, 0)]
        public async Task GetTicketsCount_ReturnsTicketsCount_WhenGivenIdExists(int idToSearch, int totalTickets)
        {
            this.dbContext.Events.Add(new Event { Id = idToSearch, TotalTickets = totalTickets });
            await dbContext.SaveChangesAsync();

            var expectedCount = totalTickets;
            var actualCount = this.eventService.GetTicketsCount(idToSearch);

            Assert.Equal(expectedCount, actualCount);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetTicketsCount_ReturnsNull_WhenGivenIdDoesNotExists(int idToSearch)
        {
            this.dbContext.Events.Add(new Event { Id = 1, TotalTickets = 3 });
            this.dbContext.Events.Add(new Event { Id = 20, TotalTickets = 3 });
            this.dbContext.Events.Add(new Event { Id = 30, TotalTickets = 3 });
            this.dbContext.Events.Add(new Event { Id = 2, TotalTickets = 3 });
            this.dbContext.Events.Add(new Event { Id = 300, TotalTickets = 3 });
            await dbContext.SaveChangesAsync();

            var actualCount = this.eventService.GetTicketsCount(idToSearch);

            Assert.Null(actualCount);
        }

        [Fact]
        public async Task GetLastAdded_ReturnsLastAddedEvent()
        {
            const int eventsCountToAdd = 5;
            for (int i = 0; i <= eventsCountToAdd; i++)
            {
                this.dbContext.Events.Add(new Event());
                await dbContext.SaveChangesAsync();
            }

            var expected = this.dbContext.Events.Last().Id;
            var actual = this.eventService.GetLastAdded().Id;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetLastAdded_ReturnsNull_WhenThereIsNoEventsInDatabase()
        {
            var actual = this.eventService.GetLastAdded()?.Id;
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(1, 1, 3)]
        [InlineData(5, 10, 30)]
        [InlineData(10, 1, 300)]
        [InlineData(10, 10, 100)]
        [InlineData(100, 5, 5)]
        public async Task BuyTickets_ReturnsTrue_WhenEventIdExistsAndTotalTicketsCountIsEnough(int idToSearch, int neededTickets, int totalTickets)
        {
            this.dbContext.Events.Add(new Event { Id = idToSearch, TotalTickets = totalTickets });
            await dbContext.SaveChangesAsync();

            Assert.True(this.eventService.BuyTickets(idToSearch, neededTickets));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task BuyTickets_ReturnsFalse_WhenGivenIdDoesNotExists(int idToSearch)
        {
            this.dbContext.Events.Add(new Event { Id = 1, TotalTickets = 100 });
            this.dbContext.Events.Add(new Event { Id = 20, TotalTickets = 100 });
            this.dbContext.Events.Add(new Event { Id = 115, TotalTickets = 100 });
            this.dbContext.Events.Add(new Event { Id = 1515, TotalTickets = 100 });
            this.dbContext.Events.Add(new Event { Id = 15615, TotalTickets = 100 });
            await dbContext.SaveChangesAsync();

            Assert.False(this.eventService.BuyTickets(idToSearch, 1));
        }

        [Theory]
        [InlineData(1, 10, 3)]
        [InlineData(5, 100, 30)]
        [InlineData(10, 10000, 300)]
        [InlineData(10, 100000, 100)]
        [InlineData(100, 6, 5)]
        public async Task BuyTickets_ReturnsTrue_WhenTotalTicketsCountIsNotEnough(int idToSearch, int neededTickets, int totalTickets)
        {
            this.dbContext.Events.Add(new Event { Id = idToSearch, TotalTickets = totalTickets });
            await dbContext.SaveChangesAsync();

            Assert.False(this.eventService.BuyTickets(idToSearch, neededTickets));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task BuyTickets_ReturnsTrue_WhenTicketsToBuyCountIs0OrNegative(int neededTickets)
        {
            this.dbContext.Events.Add(new Event { Id = 1, TotalTickets = 100 });
            await dbContext.SaveChangesAsync();

            Assert.False(this.eventService.BuyTickets(1, neededTickets));
        }

        [Fact]
        public async Task GetCount_ReturnsCorrectNumberUsingDbContext()
        {
            this.dbContext.Events.Add(new Event());
            this.dbContext.Events.Add(new Event());
            this.dbContext.Events.Add(new Event());
            await dbContext.SaveChangesAsync();

            var expected = 3;
            var actual = this.eventService.GetCount();
            Assert.Equal(expected, actual);
        }

        public class FakeEventViewModel
        {
            public int Id { get; set; }
        }
    }
}
