namespace BashSoft.Contracts
{
    public interface ICommandFactory
    {
        IExecutable CreateCommand(string commandName);
    }
}