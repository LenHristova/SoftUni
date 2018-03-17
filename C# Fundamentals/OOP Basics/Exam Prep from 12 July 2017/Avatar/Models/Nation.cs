using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Nation
{
    public Nation()
    {
        Benders = new List<Bender>();
        Monuments = new List<Monument>();
    }

    public ICollection<Bender> Benders { get; private set; }

    public ICollection<Monument> Monuments { get; private set; }

    public double Power => CalculatePower();

    private double CalculatePower()
    {
        var bendersPower = Benders.Select(b => b.TotalPower).Sum();
        var monumentsPower = Monuments.Select(m => m.Affinity).Sum();
        monumentsPower = monumentsPower == 0 ? 1 : monumentsPower/100;
        return bendersPower * monumentsPower;
    }

    public void AddBender(Bender bender)
    {
        Benders.Add(bender);
    }

    public void AddMonument(Monument monument)
    {
        Monuments.Add(monument);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("Benders:");
        if (Benders.Count == 0)
        {
            sb.AppendLine(" None");
        }
        else
        {
            sb.AppendLine();
            foreach (var bender in Benders)
            {
                sb.AppendLine($"###{bender}");
            }
        }

        sb.Append("Monuments:");
        if (Monuments.Count == 0)
        {
            sb.AppendLine(" None");
        }
        else
        {
            sb.AppendLine();
            foreach (var monument in Monuments)
            {
                sb.AppendLine($"###{monument}");
            }
        }

        return sb.ToString().TrimEnd();
    }
}