using System;

public abstract class Vehicle
{
    private const string POSITIVE_NUMBER = "{0} must be a positive number";
    private const string ZERO_OR_POSITIVE_NUMBER = "{0} must be zero or positive";

    private double _fuelQuantity;
    private double _baseFuelConsumptionInLitersPerKm;
    private double _tankCapacity;
    private double _conditionerFuelConsumptionInLitersPerKm;
    private double _fuelLossPercentage;

    protected Vehicle(double fuelQuantity, double baseFuelConsumptionInLitersPerKm, double tankCapacity)
    {
        TankCapacity = tankCapacity;
        FuelQuantity = fuelQuantity;
        BaseFuelConsumptionInLitersPerKm = baseFuelConsumptionInLitersPerKm;
    }

    public double FuelQuantity
    {
        get
        {
            return _fuelQuantity;
        }

        protected set
        {
            ValidateIsZeroOrPositive(value, nameof(FuelQuantity));
            if (value > TankCapacity)
            {
                value = 0;
            }

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

    public double TankCapacity
    {
        get
        {
            return _tankCapacity;
        }
        set
        {
            ValidateIsPositive(value, nameof(TankCapacity));
            _tankCapacity = value;
        }
    }

    public string Drive(double distance)
    {
        var neededFuel = distance * AllFuelConsumptionInLitersPerKm;

        return TryToDrive(distance, neededFuel);
    }

    protected string TryToDrive(double distance, double neededFuel)
    {
        if (FuelQuantity < neededFuel)
        {
            return $"{this.GetType()} needs refueling";
        }

        FuelQuantity -= neededFuel;
        return $"{GetType()} travelled {distance} km";
    }

    public void Refuel(double fuelAmount)
    {
        ValidateIsPositive(fuelAmount, "Fuel");

        if (fuelAmount + FuelQuantity > TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {fuelAmount} fuel in the tank");
        }

        FuelQuantity += CalcLossIndex() * fuelAmount;
    }

    private double CalcLossIndex()
    {
        return (100 - FuelLossPercentage) / 100.0;
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