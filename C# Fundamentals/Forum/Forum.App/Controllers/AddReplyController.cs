namespace Forum.App.Controllers
{
    using System.Linq;

    using Forum.App.Services;
    using Forum.App.UserInterface.Input;
    using Forum.App.UserInterface.ViewModels;
    using Forum.App.Views;
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;

    public class AddReplyController : IController
    {
        private const int TEXT_AREA_WIDTH = 37;
        private const int TEXT_AREA_HEIGHT = 6;
        private const int POST_MAX_LENGTH = 220;

        private static readonly int centerTop = Position.ConsoleCenter().Top;
        private static readonly int centerLeft = Position.ConsoleCenter().Left;

        public AddReplyController()
        {
            Post = new PostViewModel();
            this.ResetReply();
        }

        public ReplyViewModel Reply { get; private set; }

        public PostViewModel Post { get; set; }

        private TextArea TextArea { get; set; }

        public bool Error { get; set; }

        public void ResetReply()
        {
            this.Error = false;
            this.Reply = new ReplyViewModel();
            var contentLength = Post?.Content.Count ?? 0;
            this.TextArea = new TextArea(centerLeft - 18, centerTop + contentLength - 4, TEXT_AREA_WIDTH, TEXT_AREA_HEIGHT, POST_MAX_LENGTH);
        }

        public void SetPostId(int postId)
        {
            this.Post = PostService.GetPostViewModel(postId);
            this.ResetReply();
        }

        public MenuState ExecuteCommand(int index)
        {
            switch ((Command)index)
            {
                case Command.Write:
                    this.TextArea.Write();
                    this.Reply.Content = this.TextArea.Lines.ToList();
                    return MenuState.AddReply;
                case Command.Post:
                    var isValidReply = ReplyService.TrySaveReply(this.Reply, this.Post.PostId);
                    if (!isValidReply)
                    {
                        this.Error = true;
                        return MenuState.Rerender;
                    }
                    return MenuState.ReplyAdded;
            }

            throw new InvalidCommandException();
        }

        public IView GetView(string userName)
        {
            this.Reply.Author = userName;
            return new AddReplyView(this.Post, this.Reply, this.TextArea, this.Error);
        }

        private enum Command
        {
            Write,
            Post
        }
    }
}
