namespace BusTickets.Client.Core.Contracts
{
    public interface ICommand
    {
        string Execute(string[] data);
    }
}
