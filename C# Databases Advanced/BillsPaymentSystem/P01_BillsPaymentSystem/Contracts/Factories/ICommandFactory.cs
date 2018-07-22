namespace P01_BillsPaymentSystem.Contracts.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName);
    }
}