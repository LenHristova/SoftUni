using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cdrel")]
    public class ChangeRelativePathCommand : IExecutable
    {
        private readonly IDirectoryManager _inputOutputManager;

        public ChangeRelativePathCommand(IDirectoryManager inputOutputManager)
        {
            _inputOutputManager = inputOutputManager;
        }

        //Changes path relatively IF data consists 2 elements -> "cdrel" command + relative path

        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 2)
            {
                throw new InvalidCommandException(input);
            }

            var relPath = data[1];
            _inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}
