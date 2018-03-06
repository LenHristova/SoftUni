using System;

namespace P01_Vehicles.Models
{
    public abstract class Vehicle
    {
        private const string POSITIVE_NUMBER = "{0} must be a positive number";
        private const string ZERO_OR_POSITIVE_NUMBER = "{0} must be zero or positive";

        private double _fuelQuantity;
        private double _baseFuelConsumptionInLitersPerKm;
        private double _conditionerFuelConsumptionInLitersPerKm;
        private readonly double _technicalProblemsInfluenceIndex;
        private double _fuelLossPercentage;

        protected Vehicle(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double fuelLossPercentage = 0)
        {
            FuelQuantity = fuelQuantity;
            BaseFuelConsumptionInLitersPerKm = baseFuelConsumptionInLitersPerKm;
            _technicalProblemsInfluenceIndex = (100 - fuelLossPercentage) / 100.0;
        }

        public double FuelQuantity
        {
            get
            {
                return _fuelQuantity;
            }

            private set
            {
                ValidateIsZeroOrPositive(value, nameof(FuelQuantity));
                _fuelQuantity = value;
            }
        }

        public double AllFuelConsumptionInLitersPerKm => BaseFuelConsumptionInLitersPerKm + ConditionerFuelConsumptionInLitersPerKm;

        public double BaseFuelConsumptionInLitersPerKm
        {
            get { return _baseFuelConsumptionInLitersPerKm; }

            private set
            {
                ValidateIsPositive(value, nameof(BaseFuelConsumptionInLitersPerKm));

                _baseFuelConsumptionInLitersPerKm = value;
            }
        }

        public double ConditionerFuelConsumptionInLitersPerKm
        {
            get
            {
                return _conditionerFuelConsumptionInLitersPerKm;
            }

            set
            {
                ValidateIsPositive(value, nameof(ConditionerFuelConsumptionInLitersPerKm));
                _conditionerFuelConsumptionInLitersPerKm = value;
            }
        }

        public double FuelLossPercentage
        {
            get
            {
                return _fuelLossPercentage;
            }
            set
            {
                ValidateIsZeroOrPositive(value, nameof(FuelLossPercentage));
                _fuelLossPercentage = value;
            }
        }

        public string Drive(double distance)
        {
            var neededFuel = distance * AllFuelConsumptionInLitersPerKm;

            if (FuelQuantity < neededFuel)
            {
                return $"{this.GetType().Name} needs refueling";
            }

            FuelQuantity -= neededFuel;
            return $"{GetType().Name} travelled {distance} km";
        }

        public void Refuel(double fuelAmount)
        {
            FuelQuantity += _technicalProblemsInfluenceIndex * fuelAmount;
        }

        private static void ValidateIsPositive(double value, string argument)
        {
            if (value <= 0)
            {
                throw new ArgumentException(
                    string.Format(POSITIVE_NUMBER, argument));
            }
        }

        private static void ValidateIsZeroOrPositive(double value, string argument)
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    string.Format(ZERO_OR_POSITIVE_NUMBER, argument));
            }
        }
    }
}