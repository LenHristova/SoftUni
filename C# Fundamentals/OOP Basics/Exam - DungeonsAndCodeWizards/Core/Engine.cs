using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Core
{
    public class Engine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;
        private readonly IParty party;

        public Engine(IReader reader, IWriter writer, ICommandInterpreter commandInterpreter, IParty party)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
            this.party = party;
        }

        public void Run()
        {
            string input;
            while (!party.IsGameOver() && !string.IsNullOrEmpty(input = reader.ReadLine()))
            {
                try
                {
                    var output = commandInterpreter.ProcessCommand(input.Split());
                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        writer.WriteLine(output);
                    }
                }
                catch (ArgumentException ae)
                {
                    writer.WriteLine($"Parameter Error: {ae.Message}");
                }
                catch (InvalidOperationException ioe)
                {
                    writer.WriteLine($"Invalid Operation: {ioe.Message}");
                }
            }

            writer.WriteLine("Final stats:");
            writer.WriteLine(party.GetStats());
        }
    }
}
