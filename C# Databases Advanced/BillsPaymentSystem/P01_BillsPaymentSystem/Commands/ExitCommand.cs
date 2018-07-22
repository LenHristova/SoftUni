namespace P01_BillsPaymentSystem.Commands
{
	using System;
	using Contracts;
	using Contracts.Factories;
	using Contracts.Services;

    public class ExitCommand : Command
    {
        public ExitCommand(IUserService userService, IMenuFactory menuFactory) 
            : base(userService, menuFactory)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            Environment.Exit(0);
            return null;
        }
    }
}
