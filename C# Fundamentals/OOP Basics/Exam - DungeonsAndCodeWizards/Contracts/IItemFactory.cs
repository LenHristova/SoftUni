namespace DungeonsAndCodeWizards.Contracts
{
    public interface IItemFactory
    {
        IItem CreateItem(string itemName);
    }
}