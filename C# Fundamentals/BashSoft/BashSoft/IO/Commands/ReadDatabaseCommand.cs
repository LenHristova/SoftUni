using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ReadDatabaseCommand : Command, IExecutable
    {
        public ReadDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        //Reads database from file IF command consists 2 elements -> "readdb" command + file's name
        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            var fileName = Data[1];
            Repository.LoadData(fileName);
        }
    }
}