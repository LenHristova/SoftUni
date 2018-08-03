namespace BusTickets.Client.Core.Commands
{
    using System;
    using Contracts;

    public abstract class Command : ICommand
    {
        public abstract string Execute(string[] data);

        protected void EnsureParametersCount(int count, int neededCount)
        {
            if (count < neededCount)
            {
                throw new ArgumentException("Invalid parameters count!");
            }
        }

        protected void EnsureNotNull(object obj, string objName, string objDescription)
        {
            if (obj == null)
            {
                throw new InvalidOperationException($"{objName} {objDescription} not found!");
            }
        }
    }
}
