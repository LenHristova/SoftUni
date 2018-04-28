using System;

using BashSoft.Contracts;

namespace BashSoft.IO
{
    public class CommandInterpreter : IInterpreter
    {
        private readonly ICommandFactory _commandFactory;

        public CommandInterpreter(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public void InterpretCommand(string input)
        {
            var commandName = input.Split()[0];
            try
            {
                var command = _commandFactory.CreateCommand(commandName);
                command.Execute(input);
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        }
    }
}