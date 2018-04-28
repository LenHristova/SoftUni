using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Items
{
    public class HealthPotion : Item
    {
        private const int WEIGHT = 5;
        private const int HEALTH_POINTS = 20;

        public HealthPotion() : base(WEIGHT)
        {
        }

        public override void AffectCharacter(ICharacter character)
        {
            character.EnsureAlive();
            character.BeHealed(HEALTH_POINTS);
        }
    }
}
