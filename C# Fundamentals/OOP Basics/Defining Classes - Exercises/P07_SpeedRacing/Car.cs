namespace P07_SpeedRacing
{
    using System;

    public class Car
    {
        private string _model;
        private double _fuelAmount;
        private double _fuelConsumptionFor1Km;
        private double _traveledDistance;

        public string Model
        {
            get => _model;
            set => _model = value;
        }

        public double FuelAmount
        {
            get => _fuelAmount;
            set => _fuelAmount = value;
        }

        public double FuelConsumptionFor1Km
        {
            get => _fuelConsumptionFor1Km;
            set => _fuelConsumptionFor1Km = value;
        }

        public double TraveledDistance
        {
            get => _traveledDistance;
            set => _traveledDistance = value;
        }

        public Car(string model)
        {
            _model = model;
        }

        public Car(string model, double fuelAmount, double fuelConsumptionFor1Km):this(model)
        {
            _fuelAmount = fuelAmount;
            _fuelConsumptionFor1Km = fuelConsumptionFor1Km;
            _traveledDistance = 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Car item))
            {
                return false;
            }

            return Model.Equals(item.Model);
        }

        public override int GetHashCode()
        {
            return Model.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Model} {FuelAmount:F2} {TraveledDistance}";
        }

        public void Drive(int amountOfKm)
        {
            var neededFuel = amountOfKm * FuelConsumptionFor1Km;
            if ( neededFuel <= FuelAmount)
            {
                FuelAmount -= neededFuel;
                TraveledDistance += amountOfKm;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }
    }
}