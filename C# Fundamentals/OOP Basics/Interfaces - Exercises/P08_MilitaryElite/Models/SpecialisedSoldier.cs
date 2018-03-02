using System.Text;

public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
{
    public Corps Corps { get; }

    protected SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, Corps corps) : base(id, firstName, lastName, salary)
    {
        Corps = corps;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString())
            .Append($"{nameof(Corps)}: {Corps}");

        return sb.ToString();
    }
}