using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cdrel")]
    public class ChangeRelativePathCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDirectoryManager _inputOutputManager;

        public ChangeRelativePathCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        //Changes path relatively IF data consists 2 elements -> "cdrel" command + relative path
        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            var relPath = Data[1];
            _inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}
