namespace Chushka.Services.Contracts
{
    using Common.Models.Products;
    using Data.Models;
    using System.Linq;

    public interface IProductService
    {
        void Create(CreateProductInputModel model);

        IQueryable<Product> All();

        Product Get(string id);

        void Edit(EditProductInputModel model);

        void Delete(string id);
    }
}
