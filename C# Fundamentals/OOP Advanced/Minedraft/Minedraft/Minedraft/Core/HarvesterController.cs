using System;
using System.Collections.Generic;
using System.Linq;

public class HarvesterController : IHarvesterController
{
    private readonly IList<IHarvester> _harvesters;
    private readonly IEnergyRepository _energyRepository;
    private readonly IHarvesterFactory _factory;
    private Mode _mode;

    public HarvesterController(IEnergyRepository energyRepository, IHarvesterFactory factory)
    {
        _harvesters = new List<IHarvester>();
        _energyRepository = energyRepository;
        _factory = factory;
        //ChangeMode(DEFAULT_MODE);
        _mode = Mode.Full;
    }

    public double EnergyRequiments => _harvesters.Sum(h => h.EnergyRequirement);

    public double OreProduced { get; private set; }

    public IReadOnlyCollection<IEntity> Entities => (IReadOnlyCollection<IHarvester>)_harvesters;

    public string Register(IList<string> args)
    {
        var harvester = _factory.GenerateHarvester(args);
        _harvesters.Add(harvester);
        return string.Format(OutputMessages.SUCCESSFULL_REGISTRATION,
            harvester.GetType().Name);
    }

    public string Produce()
    {
        var oreProducedToday = _energyRepository.TakeEnergy(ModeEnergyRequiments())
            ? ModeMinedOre()
            : 0.0;

        OreProduced += oreProducedToday;

        return string.Format(OutputMessages.ORE_OUTPUT_TODAY, oreProducedToday);
    }

    private double ModeEnergyRequiments()=> EnergyRequiments * (int)_mode / 100;

    private double ModeMinedOre() => _harvesters.Sum(h => h.Produce()) * (int)_mode / 100;

    public string ChangeMode(string mode)
    {
        _mode = (Mode)Enum.Parse(typeof(Mode), mode);

        var reminder = new List<IHarvester>();

        foreach (var harvester in _harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception)
            {
                reminder.Add(harvester);
            }
        }

        foreach (var entity in reminder)
        {
            _harvesters.Remove(entity);
        }

        return string.Format(OutputMessages.MODE_CHANGED, mode);
    }
}