using System.Collections.Generic;
using System.Linq;

public class DraftManager
{
    private readonly HarvesterFactory _harvesterFactory;
    private readonly ProviderFactory _providerFactory;
    private readonly ICollection<Harvester> _harvesters;
    private readonly ICollection<Provider> _providers;
    private double _totalStoredEnergy;
    private double _totalStoredOre;
    private double _activeModeEnergyRequirementReduce;
    private double _activeModeOreOutputReduce;

    public DraftManager()
    {
        _harvesterFactory = new HarvesterFactory();
        _providerFactory = new ProviderFactory();
        _harvesters = new List<Harvester>();
        _providers = new List<Provider>();
        _totalStoredEnergy = 0.0;
        _totalStoredOre = 0.0;
        _activeModeEnergyRequirementReduce = 1;
        _activeModeOreOutputReduce = 1;
    }

    public string RegisterHarvester(List<string> arguments)
    {
        const string baseType = nameof(Harvester);
        return Register(baseType, arguments);
    }

    public string RegisterProvider(List<string> arguments)
    {
        const string baseType = nameof(Provider);
        return Register(baseType, arguments);
    }

    private string Register(string baseType, IList<string> minerArgs)
    {
        try
        {
            Miner miner;
            switch (baseType)
            {
                case nameof(Harvester):
                    miner = _harvesterFactory.CreateHarvester(minerArgs);
                    _harvesters.Add((Harvester)miner);
                    break;
                case nameof(Provider):
                    miner = _providerFactory.CreateProvider(minerArgs);
                    _providers.Add((Provider)miner);
                    break;
                default:
                    throw new InvalidCommandExeption();
            }
            return $"Successfully registered {miner.Type} {baseType} - {miner.Id}";
        }
        catch (InvalidPropertyExeption argEx)
        {
            return $"{baseType} is not registered, because of it's {argEx.ParamName}";
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

        return $"A day has passed.{OutputWriter.NewLine}" +
               $"Energy Provided: {dailyEnergyOutput}{OutputWriter.NewLine}" +
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
        return $"System Shutdown{OutputWriter.NewLine}" +
               $"Total Energy Stored: {_totalStoredEnergy}{OutputWriter.NewLine}" +
               $"Total Mined Plumbus Ore: {_totalStoredOre}";
    }
}