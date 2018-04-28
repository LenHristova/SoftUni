using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("dropdb")]
    public class DropDatabaseCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public DropDatabaseCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Drops database IF command consist exactly 1 element -> "dropdb" command
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 1)
            {
                throw new InvalidCommandException(input);
            }

            _repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }
}