namespace CarDealer.Services.Implementations
{
    using Contracts;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Cars;
    using Models.Parts;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<string> AllMakes()
            => this.db.Cars
                .OrderBy(c => c.Make)
                .Select(c => c.Make)
                .Distinct()
                .ToList();

        public IEnumerable<CarModel> ByMake(string make)
            => db.Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new CarModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .ToList();

        public CarWithPartsModel ById(int? id)
            => this.db.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    Parts = c.Parts
                        .Select(p => new PartModel
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToList()
                })
                .FirstOrDefault();
    }
}
