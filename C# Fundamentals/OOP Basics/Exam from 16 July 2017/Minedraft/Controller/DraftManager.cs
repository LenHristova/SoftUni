using System;
using System.Collections.Generic;
using System.Linq;

public class DraftManager
{
    private readonly ICollection<Harvester> _harvesters;
    private readonly ICollection<Provider> _providers;
    private double _totalStoredEnergy;
    private double _totalStoredOre;
    private double _activeModeEnergyRequirementReduce;
    private double _activeModeOreOutputReduce;

    public DraftManager()
    {
        _harvesters = new List<Harvester>();
        _providers = new List<Provider>();
        _totalStoredEnergy = 0.0;
        _totalStoredOre = 0.0;
        _activeModeEnergyRequirementReduce = 1;
        _activeModeOreOutputReduce = 1;
    }

    public string RegisterHarvester(List<string> arguments)
    {
        const string minerType = nameof(Harvester);
        return Register(minerType, arguments);
    }

    public string RegisterProvider(List<string> arguments)
    {
        const string minerType = nameof(Provider);
        return Register(minerType, arguments);
    }

    private string Register(string minerType, IList<string> arguments)
    {
        try
        {
            Miner miner;
            switch (minerType)
            {
                case nameof(Harvester):
                    miner = HarvesterFactory.CreateHarvester(arguments);
                    _harvesters.Add((Harvester)miner);
                    break;
                case nameof(Provider):
                    miner = ProviderFactory.CreateProvider(arguments);
                    _providers.Add((Provider)miner);
                    break;
                    default:
                        throw new NotSupportedException();
            }

            return $"Successfully registered {miner.Type} {minerType} - {miner.Id}";
        }
        catch (ArgumentException argEx)
        {
            return $"{minerType} is not registered, because of it's {argEx.ParamName}";
        }
    }

    public string Day()
    {
        var dailyEnergyOutput = _providers.Sum(p => p.EnergyOutput);
        _totalStoredEnergy += dailyEnergyOutput;

        var harvestersNeededEnergy =
            _harvesters.Sum(h => h.EnergyRequirement) * _activeModeEnergyRequirementReduce;

        var dailyOreOutput = 0.0;
        if (harvestersNeededEnergy <= _totalStoredEnergy)
        {
            dailyOreOutput = _harvesters.Sum(h => h.OreOutput) * _activeModeOreOutputReduce;

            _totalStoredEnergy -= harvestersNeededEnergy;
            _totalStoredOre += dailyOreOutput;
        }

        return $"A day has passed.{Environment.NewLine}" +
               $"Energy Provided: {dailyEnergyOutput}{Environment.NewLine}" +
               $"Plumbus Ore Mined: {dailyOreOutput}";
    }

    public string Mode(List<string> arguments)
    {
        var mode = arguments[0];
        switch (mode.ToLower())
        {
            case "full":
                _activeModeEnergyRequirementReduce = 1;
                _activeModeOreOutputReduce = 1;
                break;
            case "half":
                _activeModeEnergyRequirementReduce = 0.6;
                _activeModeOreOutputReduce = 0.5;
                break;
            case "energy":
                _activeModeEnergyRequirementReduce = 0;
                _activeModeOreOutputReduce = 0;
                break;
            default:
                return "";
        }

        return $"Successfully changed working mode to {mode} Mode";
    }

    public string Check(List<string> arguments)
    {
        var id = arguments[0];
        var currentElement = _harvesters.FirstOrDefault(h => h.Id == id)
                             ?? (Miner)_providers.FirstOrDefault(p => p.Id == id);

        return currentElement == null
            ? $"No element found with id - {id}"
            : currentElement.ToString();
    }

    public string ShutDown()
    {
        return $"System Shutdown{Environment.NewLine}" +
               $"Total Energy Stored: {_totalStoredEnergy}{Environment.NewLine}" +
               $"Total Mined Plumbus Ore: {_totalStoredOre}";
    }
}