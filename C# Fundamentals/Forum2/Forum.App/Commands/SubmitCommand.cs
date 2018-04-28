using System;

using Forum.App.Contracts;

namespace Forum.App.Commands
{
    public class SubmitCommand : ICommand
    {
        private readonly ISession session;
        private readonly IPostService postService;

        public SubmitCommand(ISession session, IPostService postService)
        {
            this.session = session;
            this.postService = postService;
        }

        public IMenu Execute(params string[] args)
        {
            int postId = int.Parse(args[0]);
            string replyContents = args[1];

            if (string.IsNullOrWhiteSpace(replyContents))
            {
                throw new ArgumentException("Reply cannot be empty!");
            }

            int userId = this.session.UserId;
            this.postService.AddReplyToPost(postId, replyContents, userId);

            return this.session.Back();
        }
    }
}