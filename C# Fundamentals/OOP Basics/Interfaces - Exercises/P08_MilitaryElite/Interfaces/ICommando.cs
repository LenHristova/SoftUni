using System.Collections.Generic;

public interface ICommando:ISpecialisedSoldier
{
    IReadOnlyCollection<Mission> Missions { get; }

    void AddMission(Mission mission);
    void CompleteMission(string missionCodeName);
}