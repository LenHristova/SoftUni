using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("mkdir")]
    public class MakeDirectoryCommand : IExecutable
    {
        private readonly IDirectoryManager _inputOutputManager;

        public MakeDirectoryCommand(IDirectoryManager inputOutputManager)
        {
            _inputOutputManager = inputOutputManager;
        }

        //Creates directory IF data consist 2 elements -> "mkdir" command + directory's name
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 2)
            {
                throw new InvalidCommandException(input);
            }

            var folderName = data[1];
            _inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
