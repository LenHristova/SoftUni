namespace Forum.App.Controllers.UserControllers
{
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;
    using Forum.App.UserInterface.Views.UserViews;

    public class SignUpController : AccountController, IController, IReadUserInfoController
    {
        private const string WANTED_ACTION = "Signup";

        public SignUpController() : base(WANTED_ACTION)
        {
        }

        public override IView GetView(string userName)
        {
            return new SignUpView(this.ErrorMesage);
        }
    }
}
