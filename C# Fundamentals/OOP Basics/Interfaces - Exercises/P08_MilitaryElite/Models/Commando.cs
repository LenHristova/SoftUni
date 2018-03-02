using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Commando : SpecialisedSoldier, ICommando
{
    private readonly ICollection<Mission> _missions;
    public IReadOnlyCollection<Mission> Missions => (IReadOnlyCollection<Mission>) _missions;

    public Commando(string id, string firstName, string lastName, decimal salary, Corps corps) : base(id, firstName, lastName, salary, corps)
    {
        _missions = new List<Mission>();
    }

    public void AddMission(Mission mission)
    {
        _missions.Add(mission);
    }

    public void CompleteMission(string missionCodeName)
    {
        var mission = Missions.FirstOrDefault(m => m.CodeName == missionCodeName);
        mission?.Complete();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString())
            .AppendLine($"{nameof(Missions)}:");

        foreach (var mission in Missions)
        {
            sb.AppendLine(mission.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}