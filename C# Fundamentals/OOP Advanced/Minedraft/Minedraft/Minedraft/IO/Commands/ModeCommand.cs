using System.Collections.Generic;

public class ModeCommand : Command
{
    private readonly IHarvesterController _harvesterController;

    public ModeCommand(IList<string> arguments, IHarvesterController harvesterController) 
        : base(arguments)
    {
        _harvesterController = harvesterController;
    }

    public override string Execute()
    {
        return _harvesterController.ChangeMode(Arguments[0]);
    }
}