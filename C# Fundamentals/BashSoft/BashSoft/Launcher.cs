using BashSoft.IO;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft
{
    class Launcher
    {
        static void Main()
        {
            var tester = new Tester();
            var ioManager = new IOManager();
            var repository = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            var currentInterpreter = new CommandInterpreter(tester, repository, ioManager);
            var reader = new InputReader(currentInterpreter);
            reader.StartReadingCommands();
        }
    }
}
