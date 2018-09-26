namespace CarDealer.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Parts;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartModel> AllOnPage(int page = 1, int pageOffset = 1)
            => this.db.Parts
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageOffset)
                .Take(pageOffset)
                .Select(p => new PartModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

        public IEnumerable<PartModel> All()
            => this.db.Parts
                .OrderBy(p => p.Name)
                .Select(p => new PartModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

        public int Count() => this.db.Parts.Count();

        public void Create(string name, int quantity, decimal price, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Quantity = quantity > 0 ? quantity : 1,
                Price = price,
                SupplierId = supplierId
            };

            this.db.Parts.Add(part);
            this.db.SaveChanges();
        }

        public PartFullInfoModel ById(int? id)
            => this.db.Parts
                .Where(p => p.Id == id)
                .Select(p => new PartFullInfoModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Supplier = p.Supplier.Name
                })
                .FirstOrDefault();

        public void Delete(int id)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            this.db.Parts.Remove(part);
            this.db.SaveChanges();
        }

        public void Edit(int id, decimal price, int quantity)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            part.Price = price;
            part.Quantity = quantity;

            this.db.SaveChanges();
        }

        public bool Exists(int id)
            => this.db.Parts
                .Any(p => p.Id == id);
    }
}
