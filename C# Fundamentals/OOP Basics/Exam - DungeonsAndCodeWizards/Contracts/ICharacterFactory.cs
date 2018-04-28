namespace DungeonsAndCodeWizards.Contracts
{
    public interface ICharacterFactory
    {
        ICharacter CreateCharacter(string faction, string type, string name);
    }
}