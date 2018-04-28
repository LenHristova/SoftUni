using DungeonsAndCodeWizards.Models.Characters;

namespace DungeonsAndCodeWizards.Contracts
{
    public interface ICharacter
    {
        double AbilityPoints { get; }
        double Armor { get; }
        IBag Bag { get; }
        double BaseArmor { get; }
        double BaseHealth { get; }
        Faction Faction { get; }
        double Health { get; }
        bool IsAlive { get; }
        string Name { get; }
        double RestHealMultiplier { get; }

        void BeHealed(double healPoints);
        void BePoisoned(double poisonPoints);
        void EnsureAlive();
        void GiveCharacterItem(IItem item, ICharacter character);
        void ReceiveItem(IItem item);
        void Rest();
        void RestoreArmor();
        void TakeDamage(double hitPoints);
        string ToString();
        void UseItem(IItem item);
        void UseItemOn(IItem item, ICharacter character);
    }
}