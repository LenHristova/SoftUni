using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("ls")]
    public class TraverseFoldersCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDirectoryManager _inputOutputManager;

        public TraverseFoldersCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        //Traverses directory. Valid input (data) consists 1 or 2 elements
        //1 element -> "ls" command (default depth is 0)
        //2 element -> "ls" command + depth
        public override void Execute()
        {
            switch (Data.Length)
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
                        var hasParsed = int.TryParse(Data[1], out var depth);
                        if (!hasParsed)
                            throw new InvalidNumberException();

                        _inputOutputManager.TraverseDirectory(depth);
                        break;
                    }
                default:
                {
                    throw new InvalidCommandException(Input);
                }
            }
        }
    }
}