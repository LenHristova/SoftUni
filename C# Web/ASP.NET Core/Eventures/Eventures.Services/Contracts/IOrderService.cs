namespace Eventures.Services.Contracts
{
    using System.Collections.Generic;

    public interface IOrderService
    {
        IEnumerable<TModel> AllByUser<TModel>(string userId);

        IEnumerable<TModel> All<TModel>();

        void Create(int eventId, string userId, int ticketsCount);
    }
}
