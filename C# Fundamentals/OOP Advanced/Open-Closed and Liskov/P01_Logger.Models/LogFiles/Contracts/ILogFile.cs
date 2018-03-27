namespace P01_Logger.Models.LogFiles.Contracts
{
    public interface ILogFile
    {
        int Size { get; }

        void WriteAllText(string line);
    }
}