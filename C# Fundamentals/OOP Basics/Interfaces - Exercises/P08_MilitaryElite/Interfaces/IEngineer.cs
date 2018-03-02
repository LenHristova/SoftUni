using System.Collections.Generic;

public interface IEngineer:ISpecialisedSoldier
{
    IReadOnlyCollection<Repair> Repairs { get; }

    void AddRapair(Repair repair);
}