namespace DungeonsAndCodeWizards.Contracts
{
    public interface ICommand
    {
        string Execute(params string[] args);
    }
}