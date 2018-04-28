using System.Collections.Generic;
using System.Linq;

public class InspectCommand : Command
{
    private readonly IHarvesterController _harvesterController;
    private readonly IProviderController _providerController;

    public InspectCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController) 
        : base(arguments)
    {
        _harvesterController = harvesterController;
        _providerController = providerController;
    }

    public override string Execute()
    {
        var id = int.Parse(Arguments[0]);
        var entity = _harvesterController.Entities.FirstOrDefault(h => h.Id == id)
                     ?? _providerController.Entities.FirstOrDefault(h => h.Id == id);

        return entity?.ToString()
               ?? string.Format(OutputMessages.ENTITY_NOT_FOUND, id);
    }
}