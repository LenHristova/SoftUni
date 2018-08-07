namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //	Login <username> <password>
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            if (this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var username = args[0];
            var password = args[1];

            var user = this.userService.GetUserByCredentials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            this.userService.Login(user);

            return string.Format(Constants.SuccessfulMessages.SuccessfulLogin, user.Username);
        }
    }
}
