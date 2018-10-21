namespace SoftUniCopy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        IEnumerable<TModel> All<TModel>();

        Task<TModel> ById<TModel>(string id);
    }
}
