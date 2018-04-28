using System.Linq;

using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cdabs")]
    public class ChangeAbsolutePathCommand : IExecutable
    {
        private readonly IDirectoryManager _inputOutputManager;

        public ChangeAbsolutePathCommand(IDirectoryManager inputOutputManager)
        {
            _inputOutputManager = inputOutputManager;
        }

        //Changes path relatively IF command consists 2 elements -> "cdabs" command + absolute path
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();

            if (data.Length <= 2)
            {
                throw new InvalidCommandException(input);
            }

            var absPath = string.Join(" ", data.Skip(1));
            _inputOutputManager.ChangeCurrentDirectoryAbsolute(absPath);
        }
    }
}