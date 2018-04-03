using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("readdb")]
    public class ReadDatabaseCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDatabase _repository;

        public ReadDatabaseCommand(string input, string[] data) 
            : base(input, data)
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
            _repository.LoadData(fileName);
        }
    }
}