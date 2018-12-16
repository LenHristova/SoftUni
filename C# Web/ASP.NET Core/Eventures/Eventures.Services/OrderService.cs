namespace Eventures.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;

        public OrderService(EventuresDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<TModel> All<TModel>()
            => this.db.Orders
                .OrderBy(e => e.OrderedOn)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();

        public IEnumerable<TModel> AllByUser<TModel>(string userId)
            => this.db.Orders
                .Where(o => o.CustomerId == userId)
                .OrderBy(e => e.Event.Start)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();

        public void Create(int eventId, string userId, int ticketsCount)
        {
            var order = new Order
            {
                CustomerId = userId,
                EventId = eventId,
                OrderedOn = DateTime.UtcNow,
                TicketsCount = ticketsCount
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();
        }
    }
}
