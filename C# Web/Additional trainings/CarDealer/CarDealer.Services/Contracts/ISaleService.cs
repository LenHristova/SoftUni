namespace CarDealer.Services.Contracts
{
    using Models.Enums;
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleModel> All(SaleType type);

        SaleFullInfoModel ById(int? id);

        int Create(int customerId, int carId, double discount);
    }
}
