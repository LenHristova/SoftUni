using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class MakeDirectoryCommand : Command, IExecutable
    {
        public MakeDirectoryCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
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
            InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
