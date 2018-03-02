using System;
using System.Text;

public class Mission
{
    private string _state;

    public string CodeName { get; }

    public string State
    {
        get => _state;
        private set
        {
            if (value != "inProgress" && value != "Finished")
            {
                throw new ArgumentException("Invalid mission state!");
            }

            _state = value;
        }
    }

    public Mission(string codeName, string state)
    {
        CodeName = codeName;
        State = state;
    }

    public void Complete()
    {
        _state = "Finished";
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"  Code Name: {CodeName} State: {State}");

        return sb.ToString();
    }
}