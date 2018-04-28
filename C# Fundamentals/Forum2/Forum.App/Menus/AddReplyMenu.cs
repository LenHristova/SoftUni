namespace Forum.App.Menus
{
	using System.Collections.Generic;

	using Models;
	using Contracts;

	public class AddReplyMenu : Menu, ITextAreaMenu, IIdHoldingMenu
    {
		//private const int AUTHOR_OFFSET = 8;
		private const int LEFT_OFFSET = 18;
		private const int TOP_OFFSET = 7;
		private const int BUTTON_OFFSET = 14;

		private readonly ILabelFactory labelFactory;
		private readonly ITextAreaFactory textAreaFactory;
		private readonly IForumReader reader;
        private ICommandFactory commandFactory;
        private IPostService postService;
        private int postId;
        private bool error;
		private IPostViewModel post;

        public AddReplyMenu(ILabelFactory labelFactory, ITextAreaFactory textAreaFactory, IForumReader reader, ICommandFactory commandFactory, IPostService postService)
        {
            this.labelFactory = labelFactory;
            this.textAreaFactory = textAreaFactory;
            this.reader = reader;
            this.commandFactory = commandFactory;
            this.postService = postService;
        }

        public ITextInputArea TextArea { get; private set; }

		protected override void InitializeStaticLabels(Position consoleCenter)
		{
			Position errorPosition = 
				new Position(consoleCenter.Left - this.post.Title.Length / 2, consoleCenter.Top - 12);
			Position titlePosition =
				new Position(consoleCenter.Left - this.post.Title.Length / 2, consoleCenter.Top - 10);
			Position authorPosition =
				new Position(consoleCenter.Left - this.post.Author.Length, consoleCenter.Top - 9);

			List<ILabel> labels = new List<ILabel>()
			{
				this.labelFactory.CreateLabel("Cannot add an empty reply!", errorPosition, !error),
				this.labelFactory.CreateLabel(this.post.Title, titlePosition),
				this.labelFactory.CreateLabel($"Author: {this.post.Author}", authorPosition),
			};

			int leftPosition = consoleCenter.Left - LEFT_OFFSET;

			int lineCount = this.post.Content.Length;

			// Add post contents
			for (int i = 0; i < lineCount; i++)
			{
				Position position = new Position(leftPosition, consoleCenter.Top - (TOP_OFFSET - i));
				ILabel label = this.labelFactory.CreateLabel(this.post.Content[i], position);
				labels.Add(label);
			}

			this.Labels = labels.ToArray();
		}

		protected override void InitializeButtons(Position consoleCenter)
		{
			int left = consoleCenter.Left + BUTTON_OFFSET;
			int top = consoleCenter.Top - (TOP_OFFSET - this.post.Content.Length);

			this.Buttons = new IButton[3];

			this.Buttons[0] = this.labelFactory.CreateButton("Write", new Position(left, top + 1));
			this.Buttons[1] = this.labelFactory.CreateButton("Submit", new Position(left - 1, top + 11));
			this.Buttons[2] = this.labelFactory.CreateButton("Back", new Position(left + 1, top + 12));
		}

		private void InitializeTextArea()
		{
			Position consoleCenter = Position.ConsoleCenter();

			int top = consoleCenter.Top - (TOP_OFFSET + this.post.Content.Length) + 5;

			this.TextArea = this.textAreaFactory.CreateTextArea(this.reader, consoleCenter.Left - 18, top, false);
		}

		public void SetId(int id)
        {
            this.postId = id;
            this.LoadPost();
        }

        private void LoadPost()
        {
            this.post = this.postService.GetPostViewModel(this.postId);

            this.InitializeTextArea();

            this.Open();
        }

        public override IMenu ExecuteCommand()
		{
            try
		    {
		        string commandName = string.Join("", CurrentOption.Text.Split());
		        ICommand command = this.commandFactory.CreateCommand(commandName);

		        IMenu view = command.Execute(this.postId.ToString(), this.TextArea.Text);

		        return view;
		    }
		    catch
		    {
		        this.error = true;
		        this.InitializeStaticLabels(Position.ConsoleCenter());
		        return this;
		    }
        }
	}
}
