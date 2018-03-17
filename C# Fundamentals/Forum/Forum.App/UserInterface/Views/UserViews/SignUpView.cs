namespace Forum.App.UserInterface.Views.UserViews
{
    using Forum.App.UserInterface.Contracts;

    internal class SignUpView : UserView, IView
    {
        private const string USER_WANTED_ACTION_BUTTON = "Sign Up";

        public SignUpView(string errorMessage) : base(errorMessage, USER_WANTED_ACTION_BUTTON)
        {
        }
    }
}
