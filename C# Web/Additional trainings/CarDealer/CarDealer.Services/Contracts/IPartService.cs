namespace CarDealer.Services.Contracts
{
    using Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartModel> AllOnPage(int page, int pageOffset);

        IEnumerable<PartModel> All();

        int Count();

        void Create(string name, int quantity, decimal price, int supplierId);

        PartFullInfoModel ById(int? id);

        void Delete(int id);

        void Edit(int id, decimal price, int quantity);

        bool Exists(int id);
    }
}
