namespace DungeonsAndCodeWizards.Contracts
{
    public interface IPool
    {
        string AddItemToPool(IItem item);
        IItem PickUpItem();
    }
}