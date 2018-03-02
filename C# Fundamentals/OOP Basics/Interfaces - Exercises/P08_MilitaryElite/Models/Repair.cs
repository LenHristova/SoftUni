using System.Text;

public class Repair
{
    public string PartName { get; }
    public int WorkedHours { get; }

    public Repair(string partName, int workedHours)
    {
        PartName = partName;
        WorkedHours = workedHours;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"  Part Name: {PartName} Hours Worked: {WorkedHours}");

        return sb.ToString();
    }
}