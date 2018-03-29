using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ChangeAbsolutePathCommand : Command, IExecutable
    {
        public ChangeAbsolutePathCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
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
            InputOutputManager.ChangeCurrentDirectoryAbsolute(absPath);
        }
    }
}