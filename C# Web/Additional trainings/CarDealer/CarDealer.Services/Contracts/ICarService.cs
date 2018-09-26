namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Cars;

    public interface ICarService
    {
        IEnumerable<string> AllMakes();

        IEnumerable<CarModel> ByMake(string make);

        CarWithPartsModel ById(int? id);
    }
}
