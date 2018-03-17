using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO.Commands
{
    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
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
                        InputOutputManager.TraverseDirectory(depth);
                        break;
                    }

                case 2:
                    {
                        //check if second element is a number
                        var hasParsed = int.TryParse(Data[1], out var depth);
                        if (!hasParsed)
                            throw new InvalidNumberException();

                        InputOutputManager.TraverseDirectory(depth);
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