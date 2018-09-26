namespace CarDealer.Services.Implementations
{
    using Contracts;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Models.Cars;
    using Models.Parts;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarBaseModel> All()
            => this.db.Cars
                .OrderBy(c => c.Make)
                .Select(c => new CarBaseModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model
                })
                .ToList();

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
                        .Select(p => new PartBaseModel
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToList()
                })
                .FirstOrDefault();

        public void Create(
            string make, 
            string model, 
            long traveledDistance, 
            IEnumerable<int> partsIds)
        {
            var car = new Car
            {
                Make = make,
                Model = model,
                TraveledDistance = traveledDistance,
                Parts = this.db.Parts
                    .Where(p => partsIds.Contains(p.Id))
                    .Select(p => new PartCar{PartId = p.Id})
                    .ToList()
            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public bool Exists(int? id)
            => this.db.Cars.Any(c => c.Id == id);

        public CarPriceModel GetCarPriceModel(int id)
            => this.db.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarPriceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Parts.Sum(p => p.Part.Price)
                })
                .FirstOrDefault();
    }
}
