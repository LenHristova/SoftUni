using System.Collections.Generic;

public class RepairCommand : Command
{
    private readonly IProviderController _providerController;

    public RepairCommand(IList<string> arguments, IProviderController providerController) 
        : base(arguments)
    {
        _providerController = providerController;
    }

    public override string Execute()
    {
        return _providerController.Repair(double.Parse(Arguments[0]));
    }
}