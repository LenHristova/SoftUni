using DungeonsAndCodeWizards.Contracts;
using DungeonsAndCodeWizards.IO.Commands;

public interface ICommandFactory
{
    ICommand CreateCommand(string commandName);
}