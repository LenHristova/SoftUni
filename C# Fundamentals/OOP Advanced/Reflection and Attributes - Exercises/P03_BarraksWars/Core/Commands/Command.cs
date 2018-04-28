using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core.Commands
{
    public abstract class Command : IExecutable
    {
        protected Command(string[] data)
        {
            Data = data;
        }

        public string[] Data { get; }

        public abstract string Execute();
    }
}
