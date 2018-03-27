using System;

using P01_Logger.Core.IO.Contracts;

namespace P01_Logger.Core.IO
{
	public class ConsoleReader : IReader
	{
		public string ReadLine()
		{
			return Console.ReadLine();
		}
    }
}
