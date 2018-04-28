using System.Collections.Generic;
using System.Linq;

using DungeonsAndCodeWizards.Contracts;
using DungeonsAndCodeWizards.IO.Commands;

namespace DungeonsAndCodeWizards.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly ICommandFactory commandFactory;

        public CommandInterpreter(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public string ProcessCommand(IList<string> args)
        {
            var commandName = args[0] + nameof(Command);
            var command = commandFactory.CreateCommand(commandName);
            var commandArgs = args.Skip(1).ToArray();
            return command.Execute(commandArgs).Trim();
        }
    }
}