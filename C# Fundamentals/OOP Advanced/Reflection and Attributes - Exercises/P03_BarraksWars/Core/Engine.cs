using System;

using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core
{
    class Engine : IRunnable
    {
        private readonly ICommandInterpreter _commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            _commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var data = Console.ReadLine().Split();
                    var commandName = data[0];
                    var command = _commandInterpreter.InterpretCommand(data, commandName);
                    var result = command.Execute();
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
