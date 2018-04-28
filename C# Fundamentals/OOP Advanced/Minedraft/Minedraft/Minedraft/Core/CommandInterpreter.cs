using System.Collections.Generic;

public class CommandInterpreter : ICommandInterpreter
{
    private readonly ICommandFactory _commandFactory;

    public IHarvesterController HarvesterController { get; }

    public IProviderController ProviderController { get; }

    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController, ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
        HarvesterController = harvesterController;
        ProviderController = providerController;
    }

    public string ProcessCommand(IList<string> args)
    {
        var command = _commandFactory.GenerateCommand(args);
        return command.Execute().Trim();
    }
}