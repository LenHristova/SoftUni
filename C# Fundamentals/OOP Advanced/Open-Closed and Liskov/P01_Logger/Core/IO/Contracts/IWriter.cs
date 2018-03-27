namespace P01_Logger.Core.IO.Contracts
{
	public interface IWriter
	{
		void WriteLine(string message);

	    void WriteGreenLine(string message);

	    void DisplayException(string message);
	}
}
