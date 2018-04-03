using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDirectoryManager _inputOutputManager;

        public MakeDirectoryCommand(string input, string[] data)
            : base(input, data)
        {
        }

        //Creates directory IF data consist 2 elements -> "mkdir" command + directory's name
        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            var folderName = Data[1];
            _inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
