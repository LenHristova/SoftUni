namespace TeamBuilder.App.Core.Contracts
{
    public interface ICommandDispatcher
    {
        string Dispatch(string input);
    }
}
