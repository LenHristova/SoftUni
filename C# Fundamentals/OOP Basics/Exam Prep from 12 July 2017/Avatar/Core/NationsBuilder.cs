using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NationsBuilder
{
    private IDictionary<string, Nation> _nations;
    private readonly IList<string> _warRecords;

    public NationsBuilder()
    {
        InitializeNations();
        _warRecords = new List<string>();
    }

    private void InitializeNations()
    {
        _nations = new Dictionary<string, Nation>
        {
            {"Air", new Nation() },
            {"Water", new Nation() },
            {"Fire", new Nation() },
            {"Earth", new Nation() },
        };
    }

    public void AssignBender(List<string> benderArgs)
    {
        var bender = BenderFactory.CreateBender(benderArgs);
        _nations[bender.Type].AddBender(bender);
    }

    public void AssignMonument(List<string> monumentArgs)
    {
        var monument = MonumentFactory.CreateMonument(monumentArgs);
        _nations[monument.Type].AddMonument(monument);
    }

    public string GetStatus(string nationsType)
    {
        return $"{nationsType} Nation{Environment.NewLine}" +
               $"{_nations[nationsType]}";
    }

    public void IssueWar(string nationsType)
    {
        var winner = _nations
            .OrderByDescending(n => n.Value.Power)
            .First();
      
        _warRecords.Add(nationsType);

        InitializeNations();
        _nations[winner.Key] = winner.Value;
    }

    public string GetWarsRecord()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < _warRecords.Count; i++)
        {
            sb.AppendLine($"War {i+1} issued by {_warRecords[i]}");
        }

        return sb.ToString().TrimEnd();
    }
}