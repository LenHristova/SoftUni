using System;

public abstract class Harvester : Miner
{
    private const double MAX_ENERGY_REQUIREMENT = 20_000;

    private double _oreOutput;
    private double _energyRequirement;

    protected Harvester(string id, double oreOutput, double energyRequirement) : base(id)
    {
        OreOutput = oreOutput;
        EnergyRequirement = energyRequirement;
    }

    public double OreOutput
    {
        get { return _oreOutput; }
        private set
        {
            Validator.ValidateNotNegative(value, nameof(OreOutput));
            _oreOutput = value;
        }
    }

    public double EnergyRequirement
    {
        get { return _energyRequirement; }
        private set
        {
            Validator.ValidateNotNegative(value, nameof(EnergyRequirement));
            Validator.ValidateMaxValue(value, MAX_ENERGY_REQUIREMENT, nameof(EnergyRequirement));
            _energyRequirement = value;
        }
    }

    public override string Type => nameof(Harvester);

    public override string ToString()
    {
        return $"{Type} Harvester - {Id}{Environment.NewLine}" +
               $"Ore Output: {OreOutput}{Environment.NewLine}" +
               $"Energy Requirement: {EnergyRequirement}";
    }
}