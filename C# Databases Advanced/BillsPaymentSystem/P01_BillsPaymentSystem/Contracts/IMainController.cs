namespace P01_BillsPaymentSystem.Contracts
{
    public interface IMainController 
    {
        IMenu OpenMenu(string menuName);

        IMenu Execute(string commandName, params string[] data);
    }
}