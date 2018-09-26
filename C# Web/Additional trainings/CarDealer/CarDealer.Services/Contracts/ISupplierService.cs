namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> AllByIsImporter(bool isImporter);

        IEnumerable<SupplierBaseModel> All();

        SupplierWithPartsModel ById(int? id);

        bool Exists(int id);
    }
}
