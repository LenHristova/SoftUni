using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cmd")]
    public class CompareFilesCommand : IExecutable
    {
        private readonly IContentComparer _judge;

        public CompareFilesCommand(IContentComparer judge)
        {
            _judge = judge;
        }

        //Compares files IF data consists 3 elements ->
        //"cmd" command + absolute path of the first file + absolute path of the second file 
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length <= 3)
            {
                throw new InvalidCommandException(input);
            }

            var firstPath = data[1];
            var secondPath = data[2];

            _judge.CompareContent(firstPath, secondPath);
        }
    }
}
