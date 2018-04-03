using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cmd")]
    public class CompareFilesCommand : Command, IExecutable
    {
        [Inject]
        private readonly IContentComparer _judge;

        public CompareFilesCommand(string input, string[] data) 
            : base(input, data)
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

            _judge.CompareContent(firstPath, secondPath);
        }
    }
}
