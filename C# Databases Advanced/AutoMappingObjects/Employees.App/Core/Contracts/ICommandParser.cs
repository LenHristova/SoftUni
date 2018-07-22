using Employees.App.Commands.Contracts;

namespace Employees.App.Core.Contracts
{
    internal interface ICommandParser
    {
        ICommand ParseCommand(string commandName);
    }
}