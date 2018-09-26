namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Enums;
    using Models.Sales;

    public interface ISaleService
    {
        IEnumerable<SaleModel> All(SaleType type);

        SaleFullInfoModel ById(int? id);
    }
}
