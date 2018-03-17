namespace Forum.App.Controllers.UserControllers
{
    using System;

    using Forum.App.Controllers.Contracts;
    using Forum.App.Services;
    using Forum.App.UserInterface;
    using Forum.App.UserInterface.Contracts;

    public abstract class AccountController : IController
    {
        private const string DETAILS_ERROR = "Invalid Username or Password!";
        private const string USERNAME_TAKEN_ERROR = "Username already in use!";

        protected AccountController(string wantedAction)
        {
            WantedAction = wantedAction;
            this.Reset();
        }

        public string Username { get; private set; }

        protected string Password { get; set; }

        protected string ErrorMesage { get; set; }

        protected void Reset()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.ErrorMesage = string.Empty;
        }

        public void ReadPassword()
        {
            this.Password = ForumViewEngine.ReadRow();
            ForumViewEngine.HideCursor();
        }

        public void ReadUsername()
        {
            this.Username = ForumViewEngine.ReadRow();
            ForumViewEngine.HideCursor();
        }

        protected string WantedAction { get; set; }

        public MenuState ExecuteCommand(int index)
        {
            switch ((Command)index)
            {
                case Command.ReadUsername:
                    this.ReadUsername();
                    return GetMenuState();

                case Command.ReadPassword:
                    this.ReadPassword();                    
                    return GetMenuState(); 
                case Command.WantedAction:
                    return HasSuccesfullUserAction();
                case Command.Back:
                    this.Reset();
                    return MenuState.Back;
            }

            throw new InvalidCommandException();
        }

        private MenuState GetMenuState()
        {
            return (MenuState)Enum.Parse(typeof(MenuState), WantedAction);
        }

        protected MenuState HasSuccesfullUserAction()
        {
            var hasSuccessfulAction = false;
            var actionStatus = UserService.TryExecuteAction(this.Username, this.Password, WantedAction);
            switch (actionStatus)
            {
                case ActionStatus.Success:
                    hasSuccessfulAction = true;
                    break;
                case ActionStatus.DetailsError:
                    this.ErrorMesage = DETAILS_ERROR;
                    break;
                case ActionStatus.UsernameTakenError:
                    this.ErrorMesage = USERNAME_TAKEN_ERROR;
                    break;
            }

            return hasSuccessfulAction
                ? MenuState.SuccessfulLogIn
                : MenuState.Error;
        }

        public enum ActionStatus
        {
            Success,
            DetailsError,
            UsernameTakenError
        }

        private enum Command
        {
            ReadUsername,
            ReadPassword,
            WantedAction,
            Back
        }

        public abstract IView GetView(string userName);
    }
}
