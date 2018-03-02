using System.Text;

public class Spy : Soldier, ISpy
{
    public int CodeNumber { get; }

    public Spy(string id, string firstName, string lastName, int codeNumber):base(id, firstName, lastName)
    {
        CodeNumber = codeNumber;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString())
            .Append($"Code Number: {CodeNumber}");

        return sb.ToString();
    }
}