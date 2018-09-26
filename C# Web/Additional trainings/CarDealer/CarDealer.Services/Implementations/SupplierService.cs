namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models.Parts;
    using Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All(bool isImporter)
            => this.db.Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

        public SupplierWithPartsModel ById(int? id)
            => this.db.Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierWithPartsModel
                {
                    Name = s.Name,
                    Type = s.IsImporter ? "Importer" : "Local",
                    Parts = s.Parts
                        .Select(p => new SupplierPartModel
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Quantity = p.Quantity
                        })
                        .ToList()
                })
                .FirstOrDefault();
    }
}
