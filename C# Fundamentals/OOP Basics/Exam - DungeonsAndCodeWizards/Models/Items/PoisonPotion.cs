using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Items
{
    public class PoisonPotion:Item
    {
        private const int WEIGHT = 5;
        private const int POISON_POINTS = 20;

        public PoisonPotion() : base(WEIGHT)
        {
        }

        public override void AffectCharacter(ICharacter character)
        {
            character.EnsureAlive();
            character.BePoisoned(POISON_POINTS);
        }
    }
}
