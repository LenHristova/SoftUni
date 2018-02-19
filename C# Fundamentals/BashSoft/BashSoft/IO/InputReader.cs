using System;

namespace BashSoft.IO
{
    public class InputReader
    {
        private const string EndCommand = "quit";
        private readonly CommandInterpreter _interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            _interpreter = interpreter;
        }

        //Listening for commands and executes them if the syntax is correct
        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            string input;
            while ((input = Console.ReadLine()?.Trim()) != EndCommand)
            {
                //Interpret command
                _interpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            }
        }
    }
}
