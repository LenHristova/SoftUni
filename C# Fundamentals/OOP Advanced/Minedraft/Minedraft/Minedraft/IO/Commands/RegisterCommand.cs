using System.Collections.Generic;
using System.Linq;

public class RegisterCommand : Command
{
    private readonly IHarvesterController _harvesterController;
    private readonly IProviderController _providerController;

    public RegisterCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController) 
        : base(arguments)
    {
        _harvesterController = harvesterController;
        this._providerController = providerController;
    }

    public override string Execute()
    {
        var entityArgs = Arguments.Skip(1).ToList();
        string result = null;
        if (Arguments[0] == nameof(Harvester))
        {
            result = _harvesterController.Register(entityArgs);
        }
        else if (Arguments[0] == nameof(Provider))
        {
            result = _providerController.Register(entityArgs);
        }

        return result;
    }
}