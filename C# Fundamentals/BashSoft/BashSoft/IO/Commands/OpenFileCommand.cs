using System.Diagnostics;
using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO.Commands
{
    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        //Opens file IF data consist 2 elements -> "open" command + file's name
        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            var fileName = Data[1];
            var open = new Process
            {
                StartInfo =
                {
                    UseShellExecute = true,
                    FileName = SessionData.currentPath + "\\" + fileName
                }
            };

            open.Start();
        }
    }
}
