using Forum.App.UserInterface.Contracts;

namespace Forum.App.UserInterface.Views.UserViews
{
    internal class LogInView : UserView, IView
    {
        private const string USER_WANTED_ACTION_BUTTON = "Log In";

        public LogInView(string errorMessage) : base(errorMessage, USER_WANTED_ACTION_BUTTON)
        {
        }
    }
}