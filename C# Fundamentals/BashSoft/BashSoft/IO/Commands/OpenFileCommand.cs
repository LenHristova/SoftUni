using System.Diagnostics;

using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("open")]
    public class OpenFileCommand : IExecutable
    {
        public OpenFileCommand()
        {
        }

        //Opens file IF data consist 2 elements -> "open" command + file's name
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 2)
            {
                throw new InvalidCommandException(input);
            }

            var fileName = data[1];
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
