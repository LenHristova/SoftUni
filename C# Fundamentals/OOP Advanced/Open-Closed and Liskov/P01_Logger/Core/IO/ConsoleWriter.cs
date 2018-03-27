using System;

using P01_Logger.Core.IO.Contracts;

namespace P01_Logger.Core.IO
{
	public class ConsoleWriter : IWriter
	{
		public void WriteLine(string message)
		{
			Console.WriteLine(message);
		}

	    //Changes currently set output text color to green, 
	    //writes a message and then reverses the color
	    public void WriteGreenLine(string message)
	    {
	        var currentColor = Console.ForegroundColor;
	        Console.ForegroundColor = ConsoleColor.Green;
	        Console.WriteLine(message);
	        Console.ForegroundColor = currentColor;
	    }

	    //Changes currently set output text color to red, 
	    //writes a message and then reverses the color
	    public void DisplayException(string message)
	    {
	        var currentColor = Console.ForegroundColor;
	        Console.ForegroundColor = ConsoleColor.Red;
	        Console.WriteLine(message);
	        Console.ForegroundColor = currentColor;
	    }
    }
}