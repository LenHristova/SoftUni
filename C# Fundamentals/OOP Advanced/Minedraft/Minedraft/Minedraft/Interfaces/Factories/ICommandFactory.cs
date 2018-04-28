using System.Collections.Generic;

public interface ICommandFactory
{
    ICommand GenerateCommand(IList<string> args);
}