using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace P01_RawData
{
    public class RawData
    {
        public List<Car> Cars { get; set; } = new List<Car>();

        public void AddCar(string carInfo)
        {
            string[] parameters = carInfo.Split();
            if(parameters.Length < 9)
                throw new InvalidDataException("Car info is not full!");

            string model = parameters[0];

            int engineSpeed = int.Parse(parameters[1]);
            int enginePower = int.Parse(parameters[2]);
            Engine engine = new Engine(engineSpeed, enginePower);

            int cargoWeight = int.Parse(parameters[3]);
            string cargoType = parameters[4];
            Cargo cargo = new Cargo(cargoWeight, cargoType);

            string[] tiresSpecifics = parameters.Skip(5).Take(8).ToArray();
            TiresSet tiresSet = new TiresSet(tiresSpecifics);

            Cars.Add(new Car(model, engine, cargo, tiresSet));
        }

        public string GetCarsByCargoType(string cargoType)
        {
            if (cargoType == "fragile")
            {
                Func<Car, bool> fragileWithBadTires = x => x.Cargo.Type == "fragile" && x.TiresSet.HasTiresWithNotEnoughtPressure();

                return string.Join(Environment.NewLine, 
                    GetCarsByCriteria(fragileWithBadTires));
            }
            else
            {
                Func<Car, bool> poweredFlamable = x => x.Cargo.Type == "flamable" && x.Engine.Power > 250;

                return string.Join(Environment.NewLine,
                    GetCarsByCriteria(poweredFlamable));
            }
        }

        private List<Car> GetCarsByCriteria(Func<Car, bool> criteria)
        {
            return Cars
                .Where(criteria)
                //.Select(x => x.model)
                .ToList();
        }
    }
}