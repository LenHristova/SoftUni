using System.Collections.Generic;

namespace DungeonsAndCodeWizards.Contracts
{
    public interface ICommandInterpreter
    {
        string ProcessCommand(IList<string> args);
    }
}