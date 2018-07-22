namespace P01_BillsPaymentSystem.Contracts.Factories
{
    public interface IMenuFactory
    {
        IMenu CreateMenu(string menuName, params object[] models);
    }
}