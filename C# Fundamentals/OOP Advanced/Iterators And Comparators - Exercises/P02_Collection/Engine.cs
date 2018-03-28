using System;

namespace P02_Collection
{
    public class Engine
    {
        private readonly ListyIterator<string> _listyIterator;

        public Engine(ListyIterator<string> listyIterator)
        {
            _listyIterator = listyIterator;
        }

        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    ParseCommand(input);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch { }
            }
        }

        private void ParseCommand(string input)
        {
            var commandArgs = input.Split();
            var command = Enum.Parse<Command>(commandArgs[0]);
            switch (command)
            {
                case Command.Move:
                    Console.WriteLine(_listyIterator.Move());
                    break;
                case Command.HasNext:
                    Console.WriteLine(_listyIterator.HasNext());
                    break;
                case Command.Print:
                    _listyIterator.Print();
                    break;
                case Command.PrintAll:
                    Console.WriteLine(string.Join(" ", _listyIterator));
                    break;
                default:
                    throw new NotSupportedException();
            }

        }

        private enum Command
        {
            Move,
            HasNext,
            Print,
            PrintAll
        }
    }
}