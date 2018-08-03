namespace BusTickets.Client.Core.Contracts
{
    public interface IWriter
    {
        void Write(string output);

        void WriteLine();

        void WriteLine(string output);

        void WriteErrorMessage(string output);

        void Clear();
    }
}