namespace BashSoft
{
    using System;

    public static class InputReader
    {
        private const string endCommand = "quit";

        //Listening for commands and executes them if the syntax is correct
        public static void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            var input = Console.ReadLine().Trim();

            while (input != endCommand)
            {
                //Interpret command
                CommandInterpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine().Trim();
            }
        }
    }
}
