namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> All(bool isImporter);

        SupplierWithPartsModel ById(int? id);
    }
}
