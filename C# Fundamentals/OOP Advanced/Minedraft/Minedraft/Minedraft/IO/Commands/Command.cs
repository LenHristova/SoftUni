using System.Collections.Generic;

public abstract class Command : ICommand
{
    protected Command(IList<string> arguments)
    {
        Arguments = arguments;
    }

    public IList<string> Arguments { get; }

    public abstract string Execute();
}