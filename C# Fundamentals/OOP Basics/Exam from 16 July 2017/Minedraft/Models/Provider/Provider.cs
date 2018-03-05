using System;

public abstract class Provider : Miner
{
    private const double MAX_ENERGY_OUTPUT = 9_999;

    private double _energyOutput;

    protected Provider(string id, double energyOutput) : base(id)
    {
        EnergyOutput = energyOutput;
    }

    public double EnergyOutput
    {
        get
        {
            return _energyOutput;
        }
        protected set
        {
            Validator.ValidateIsPositive(value, nameof(EnergyOutput));
            Validator.ValidateMaxValue(value, MAX_ENERGY_OUTPUT, nameof(EnergyOutput));
            _energyOutput = value;
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name.Replace("Provider", "")} Provider - {Id}{Environment.NewLine}" +
               $"Energy Output: { EnergyOutput}";
    }
}