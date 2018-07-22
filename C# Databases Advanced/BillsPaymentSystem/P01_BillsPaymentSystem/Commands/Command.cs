namespace P01_BillsPaymentSystem.Commands
{
    using System;
    using Contracts;
    using Contracts.Factories;
    using Contracts.Services;

    public abstract class Command : ICommand
    {
        protected readonly IUserService userService;
        protected readonly IMenuFactory menuFactory;

        protected Command(IUserService userService, IMenuFactory menuFactory)
        {
            this.userService = userService;
            this.menuFactory = menuFactory;
        }

        public abstract IMenu Execute(params string[] args);

        protected void EnsureEnoughtArgs(string[] args, int neededCount)
        {
            if (args.Length < neededCount)
            {
                throw new ArgumentException("Not enought arguments.");
            }
        }
    }
}
