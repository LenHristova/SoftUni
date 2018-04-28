using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Items
{
    public class ArmorRepairKit : Item
    {
        private const int WEIGHT = 10;

        public ArmorRepairKit() : base(WEIGHT)
        {
        }

        public override void AffectCharacter(ICharacter character)
        {
            character.EnsureAlive();
            character.RestoreArmor();
        }
    }
}
