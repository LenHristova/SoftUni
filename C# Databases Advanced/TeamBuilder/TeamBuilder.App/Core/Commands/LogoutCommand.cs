namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
	using Services.Contracts;
	using Utilities;

    public class LogoutCommand : ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //	Logout
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var loggedInUser = this.userService.GetCurrentUser();

            this.userService.Logout();

            return string.Format(Constants.SuccessfulMessages.SuccessfulLogout, loggedInUser.Username);
        }
    }
}
