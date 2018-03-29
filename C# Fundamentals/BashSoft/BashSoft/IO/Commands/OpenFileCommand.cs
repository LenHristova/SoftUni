using System.Diagnostics;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class OpenFileCommand : Command, IExecutable
    {
        public OpenFileCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
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
                    FileName = SessionData.CurrentPath + "\\" + fileName
                }
            };

            open.Start();
        }
    }
}
