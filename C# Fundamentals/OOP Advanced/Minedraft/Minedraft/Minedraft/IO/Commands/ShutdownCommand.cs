using System.Collections.Generic;
using System.Text;

public class ShutdownCommand : Command
{
    private readonly IHarvesterController _harvesterController;
    private readonly IProviderController _providerController;

    public ShutdownCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController) 
        : base(arguments)
    {
        _harvesterController = harvesterController;
        _providerController = providerController;
    }

    public override string Execute()
    {
        var sb = new StringBuilder();
        sb.AppendLine(OutputMessages.SYSTEM_SHUTDOWN);
        sb.AppendLine(string.Format(OutputMessages.ENERGY_PRODUCED, _providerController.TotalEnergyProduced));
        sb.Append(string.Format(OutputMessages.MINED_ORE, _harvesterController.OreProduced));

        return sb.ToString();
    }
}