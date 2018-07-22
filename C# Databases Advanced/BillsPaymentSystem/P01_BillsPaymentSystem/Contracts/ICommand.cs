namespace P01_BillsPaymentSystem.Contracts
{
    public interface ICommand
    {
        IMenu Execute(params string[] args);
    }
}