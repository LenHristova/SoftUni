using P01_Logger.Core;
using P01_Logger.Core.IO;
using P01_Logger.Core.IO.Contracts;

namespace P01_Logger
{
    public class StartUp
    {
        static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            writer.WriteGreenLine("Insert appenders count:");
            var appendersCount = int.Parse(reader.ReadLine());
            var engine = new Engine(reader, writer, appendersCount);
            engine.Run();
        }
    }
}
