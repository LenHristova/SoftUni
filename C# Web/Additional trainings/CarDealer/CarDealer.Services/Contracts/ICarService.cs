namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarBaseModel> All();

        IEnumerable<string> AllMakes();

        IEnumerable<CarModel> ByMake(string make);

        CarWithPartsModel ById(int? id);

        void Create(
            string make,
            string model,
            long traveledDistance,
            IEnumerable<int> partsIds);

        bool Exists(int? id);

        CarPriceModel GetCarPriceModel(int id);
    }
}
