namespace Forum.App.UserInterface.Views.UserViews
{
    using Forum.App.UserInterface.Contracts;

    internal abstract class UserView : IView
    {
        private const string BACK_BUTTON = "Back";

        protected UserView(string errorMessage, string userWantedActionButton)
        {
            this.UserWantedActionButton = userWantedActionButton;
            this.ErrorMessage = errorMessage;
            InitializeLabels();
        }

        private string ErrorMessage { get; }

        private bool Error => !string.IsNullOrWhiteSpace(this.ErrorMessage);

        private string UserWantedActionButton { get; }

        public ILabel[] Labels { get; private set; }

        public ILabel[] Buttons { get; private set; }

        protected void InitializeLabels()
        {
            Position consoleCenter = Position.ConsoleCenter();

            InitializeStaticLabels(consoleCenter);

            InitializeButtons(consoleCenter);
        }

        private void InitializeStaticLabels(Position consoleCenter)
        {
            string[] labelContents = new string[] { this.ErrorMessage, "Name:", "Password:" };

            Position[] labelPositions = new Position[]
            {
                new Position(consoleCenter.Left - 16, consoleCenter.Top - 14),   // Error
                new Position(consoleCenter.Left - 16, consoleCenter.Top - 10),   // Name:
                new Position(consoleCenter.Left - 16, consoleCenter.Top - 8),    // Password:
            };

            this.Labels = new ILabel[labelContents.Length];

            this.Labels[0] = new Label(labelContents[0], labelPositions[0], !Error);

            for (int i = 1; i < this.Labels.Length; i++)
            {
                this.Labels[i] = new Label(labelContents[i], labelPositions[i]);
            }
        }

        private void InitializeButtons(Position consoleCenter)
        {
            string[] buttonContents = new string[]
            {
                " ", " ", this.UserWantedActionButton, BACK_BUTTON
            };

            Position[] buttonPositions = new Position[]
            {
                new Position(consoleCenter.Left - 10, consoleCenter.Top - 10), // Name
                new Position(consoleCenter.Left - 6, consoleCenter.Top - 8),   // Password
                new Position(consoleCenter.Left + 16, consoleCenter.Top),      // LogIn / SignUp
                new Position(consoleCenter.Left + 16, consoleCenter.Top + 1)   // Back
            };

            this.Buttons = new ILabel[buttonContents.Length];

            for (int i = 0; i < this.Buttons.Length; i++)
            {
                this.Buttons[i] = new Label(buttonContents[i], buttonPositions[i]);
            }
        }
    }
}