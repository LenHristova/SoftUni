using System.Collections.Generic;
using System.Linq;

public class Army : IArmy
{
    private IList<ISoldier> _soldiers;

    public Army()
    {
        _soldiers = new List<ISoldier>();
    }

    public IReadOnlyList<ISoldier> Soldiers => (IReadOnlyList<ISoldier>) _soldiers;

    public void AddSoldier(ISoldier soldier)
    {
        _soldiers.Add(soldier);
    }

    public void RegenerateTeam(string soldierType)
    {
        var soldiersToRegenerate = _soldiers
            .Where(s => s.GetType().Name == soldierType);
        foreach (var soldier in soldiersToRegenerate)
        {
            soldier.Regenerate();
        }
    }
}