namespace Chushka.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly ChushkaDbContext db;

        public OrderService(ChushkaDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Order> All() => this.db.Orders;

        public void Create(string productId, string userId)
        {
            var order = new Order
            {
                ProductId = productId,
                ClientId = userId,
                OrderedOn = DateTime.UtcNow
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();
        }
    }
}
