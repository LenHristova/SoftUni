using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class ChangeRelativePathCommand : Command, IExecutable
    {
        public ChangeRelativePathCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
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
            InputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}
