using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class CompareFilesCommand : Command, IExecutable
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        //Compares files IF data consists 3 elements ->
        //"cmd" command + absolute path of the first file + absolute path of the second file 
        public override void Execute()
        {
            if (Data.Length != 3)
            {
                throw new InvalidCommandException(Input);
            }

            var firstPath = Data[1];
            var secondPath = Data[2];

            Judge.CompareContent(firstPath, secondPath);
        }
    }
}
