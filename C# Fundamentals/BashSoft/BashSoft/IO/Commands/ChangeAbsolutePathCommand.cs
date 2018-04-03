using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cdabs")]
    public class ChangeAbsolutePathCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDirectoryManager _inputOutputManager;

        public ChangeAbsolutePathCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        //Changes path relatively IF command consists 2 elements -> "cdabs" command + absolute path
        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            var absPath = Data[1];
            _inputOutputManager.ChangeCurrentDirectoryAbsolute(absPath);
        }
    }
}