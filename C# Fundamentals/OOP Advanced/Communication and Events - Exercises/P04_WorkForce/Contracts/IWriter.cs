namespace P04_WorkForce.Contracts
{
    public interface IWriter
    {
        void WriteLine(string message);

        void WriteLine(int message);

        void Write(string message);

        void Write(int message);
    }
}