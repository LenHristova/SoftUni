using Forum.App.Contracts;
using Forum.App.Menus;

namespace Forum.App.Commands
{
    public class PostCommand : ICommand
    {
        private readonly ISession session;
        private readonly IPostService postService;
        private readonly ICommandFactory commandFactory;

        public PostCommand(ISession session, IPostService postService, ICommandFactory commandFactory)
        {
            this.session = session;
            this.postService = postService;
            this.commandFactory = commandFactory;
        }

        public IMenu Execute(params string[] args)
        {
            int userId = this.session.UserId;

            string postTitle = args[0];
            string postCategory = args[1];
            string postContent = args[2];

            int postId = this.postService.AddPost(userId, postTitle, postCategory, postContent);

            this.session.Back();

            ICommand command = this.commandFactory.CreateCommand(nameof(ViewPostMenu));
            IMenu menu = command.Execute(postId.ToString());

            return menu;
        }
    }
}