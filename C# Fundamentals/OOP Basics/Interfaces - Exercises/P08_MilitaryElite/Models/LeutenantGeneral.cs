using System.Collections.Generic;
using System.Text;

public class LeutenantGeneral : Private, ILeutenantGeneral
{
    private readonly ICollection<IPrivate> _privates;
    public IReadOnlyCollection<IPrivate> Privates => (IReadOnlyCollection<IPrivate>) _privates;

    public LeutenantGeneral(string id, string firstName, string lastName, decimal salary) : base(id, firstName, lastName, salary)
    {
        _privates = new List<IPrivate>();
    }

    public void AddPrivate(IPrivate @private)
    {
        _privates.Add(@private);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(base.ToString())
            .AppendLine($"{nameof(Privates)}:");
        foreach (var @private in Privates)
        {
            sb.AppendLine($"  {@private}");
        }

        return sb.ToString().TrimEnd();
    }
}