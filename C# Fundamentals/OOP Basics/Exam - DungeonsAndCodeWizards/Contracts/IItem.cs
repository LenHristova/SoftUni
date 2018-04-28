namespace DungeonsAndCodeWizards.Contracts
{
    public interface IItem
    {
        string Name { get; }
        int Weight { get; }

        void AffectCharacter(ICharacter character);
    }
}