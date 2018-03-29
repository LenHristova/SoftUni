using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class DropDatabaseCommand : Command, IExecutable
    {
        public DropDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        //Drops database IF command consist exactly 1 element -> "dropdb" command
        public override void Execute()
        {
            if (Data.Length != 1)
            {
                throw new InvalidCommandException(Input);
            }

            Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }
}