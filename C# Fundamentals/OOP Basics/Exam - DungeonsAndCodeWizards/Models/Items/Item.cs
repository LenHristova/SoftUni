using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Items
{
    public abstract class Item : IItem
    {
        protected Item(int weight)
        {
            Weight = weight;
        }

        public string Name => GetType().Name;

        public int Weight { get; }

        public abstract void AffectCharacter(ICharacter character);
    }
}
