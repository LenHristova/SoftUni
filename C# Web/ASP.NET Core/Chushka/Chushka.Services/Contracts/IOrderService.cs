namespace Chushka.Services.Contracts
{
    using Data.Models;
    using System.Linq;

    public interface IOrderService
    {
        IQueryable<Order> All();

        void Create(string productId, string userId);
    }
}
