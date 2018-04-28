using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class AddItemToPoolCommand : Command
    {
        private readonly IPool pool;
        private readonly IItemFactory itemFactory;

        public AddItemToPoolCommand(IPool pool, IItemFactory itemFactory) 
        {
            this.pool = pool;
            this.itemFactory = itemFactory;
        }

        public override string Execute(params string[] args)
        {
            var itemName = args[0];
            var item = itemFactory.CreateItem(itemName);
            return pool.AddItemToPool(item);
        }
    }
}