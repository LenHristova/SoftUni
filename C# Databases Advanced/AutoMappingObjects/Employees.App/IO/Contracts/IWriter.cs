namespace Employees.App.IO.Contracts
{
    internal interface IWriter
    {
        void Write(string output);

        void WriteLine();

        void WriteLine(string output);

        void WriteErrorMessage(string output);

        void Clear();
    }
}