namespace P01_BillsPaymentSystem.Contracts.Models
{
    public interface IWriter
    {
        void Write(string output);

        void WriteLine();

        void WriteLine(string output);

        void WriteSpecialMessage(string output);

        void WriteErrorMessage(string output);

        void WriteHelperMessage(string output);

        void Clear();
    }
}