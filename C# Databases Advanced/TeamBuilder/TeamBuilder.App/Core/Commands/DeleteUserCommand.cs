namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //	DeleteUser
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var user = this.userService.DeleteUser();

            return string.Format(Constants.SuccessfulMessages.SuccessfulDeleteUser, user.Username);
        }
    }
}
