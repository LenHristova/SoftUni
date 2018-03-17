namespace Forum.App.Controllers.UserControllers
{
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;
    using Forum.App.UserInterface.Views.UserViews;

    public class LogInController : AccountController, IController, IReadUserInfoController
    {
        private const string WANTED_ACTION = "Login";

        public LogInController() : base(WANTED_ACTION)
        {
        }

        public override IView GetView(string userName)
        {
            return new LogInView(this.ErrorMesage);
        }
    }
}
