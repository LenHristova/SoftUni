using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("readdb")]
    public class ReadDatabaseCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public ReadDatabaseCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Reads database from file IF command consists 2 elements -> "readdb" command + file's name
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 2)
            {
                throw new InvalidCommandException(input);
            }

            var fileName = data[1];
            _repository.LoadData(fileName);
        }
    }
}