using System;
using System.Collections.Generic;
using System.Text;

public class Engineer : SpecialisedSoldier, IEngineer
{
    private readonly ICollection<Repair> _repairs;
    public IReadOnlyCollection<Repair> Repairs => (IReadOnlyCollection<Repair>) _repairs;

    public Engineer(string id, string firstName, string lastName, decimal salary, Corps corps) : base(id, firstName, lastName, salary, corps)
    {
        _repairs = new List<Repair>();
    }

    public void AddRapair(Repair repair)
    {
        _repairs.Add(repair);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString())
            .AppendLine($"{nameof(Repairs)}:");

        foreach (var repair in Repairs)
        {
            sb.AppendLine(repair.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}