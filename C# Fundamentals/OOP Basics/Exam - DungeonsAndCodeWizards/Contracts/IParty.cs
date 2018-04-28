namespace DungeonsAndCodeWizards.Contracts
{
    public interface IParty
    {
        string EndTurn(string[] args);
        ICharacter FindCharacter(string characterName);
        string GetStats();
        bool IsGameOver();
        string JoinParty(ICharacter character);
    }
}