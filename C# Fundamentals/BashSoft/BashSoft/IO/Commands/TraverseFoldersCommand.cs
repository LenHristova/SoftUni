using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("ls")]
    public class TraverseFoldersCommand : IExecutable
    {
        private readonly IDirectoryManager _inputOutputManager;

        public TraverseFoldersCommand(IDirectoryManager inputOutputManager)
        {
            _inputOutputManager = inputOutputManager;
        }

        //Traverses directory. Valid input (data) consists 1 or 2 elements
        //1 element -> "ls" command (default depth is 0)
        //2 element -> "ls" command + depth
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            switch (data.Length)
            {
                case 1:
                    {
                        var depth = 0;
                        _inputOutputManager.TraverseDirectory(depth);
                        break;
                    }
                case 2:
                    {
                        //check if second element is a number
                        var hasParsed = int.TryParse(data[1], out var depth);
                        if (!hasParsed)
                            throw new InvalidNumberException();

                        _inputOutputManager.TraverseDirectory(depth);
                        break;
                    }
                default:
                {
                    throw new InvalidCommandException(input);
                }
            }
        }
    }
}