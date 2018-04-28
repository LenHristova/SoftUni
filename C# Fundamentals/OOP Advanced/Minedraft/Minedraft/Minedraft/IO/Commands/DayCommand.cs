using System;
using System.Collections.Generic;

public class DayCommand : Command
{
    private readonly IHarvesterController _harvesterController;
    private readonly IProviderController _providerController;

    public DayCommand(IList<string> arguments, IHarvesterController harvesterController, IProviderController providerController)
        : base(arguments)
    {
        _harvesterController = harvesterController;
        _providerController = providerController;
    }

    public override string Execute()
    {
        var providerResult = _providerController.Produce();
        var harvesterResult = _harvesterController.Produce();

        return providerResult + Environment.NewLine + harvesterResult;
    }
}