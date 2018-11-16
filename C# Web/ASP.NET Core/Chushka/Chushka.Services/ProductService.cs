namespace Chushka.Services
{
    using Common.Models.Products;
    using Contracts;
    using Data;
    using Data.Models;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly ChushkaDbContext db;

        public ProductService(ChushkaDbContext db)
        {
            this.db = db;
        }

        public void Create(CreateProductInputModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Type = model.Type,
                IsDeleted = false
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public IQueryable<Product> All() => this.db.Products.Where(p => !p.IsDeleted);

        public Product Get(string id) => this.db.Products.Find(id);

        public void Edit(EditProductInputModel model)
        {
            var product = this.db.Products.Find(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Type = model.Type;

                this.db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var product = this.db.Products.Find(id);

            if (product != null)
            {
                product.IsDeleted = true;
                this.db.SaveChanges();
            }
        }
    }
}
