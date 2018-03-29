using System;

using BashSoft.Contracts;

namespace BashSoft.IO
{
    public class InputReader : IReader
    {
        private const string END_COMMAND = "quit";

        private readonly IInterpreter _interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this._interpreter = interpreter;
        }

        //Listening for commands and executes them if the syntax is correct
        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
            string input;
            while ((input = Console.ReadLine()?.Trim()) != END_COMMAND)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    //Interpret command
                    _interpreter.InterpretCommand(input);
                }
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
            }
        }
    }
}
