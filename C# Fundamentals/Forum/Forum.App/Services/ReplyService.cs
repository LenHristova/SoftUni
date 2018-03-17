namespace Forum.App.Services
{
    using System.Linq;

    using Forum.App.UserInterface.ViewModels;
    using Forum.Data;
    using Forum.Models;

    public class ReplyService
    {
        public static bool TrySaveReply(ReplyViewModel replyViewModel, int postId)
        {
            var isEmptyContent = !replyViewModel.Content.Any();

            if (isEmptyContent)
            {
                return false;
            }

            var forumData = new ForumData();

            var lastAddedReplyId = forumData.Replies.LastOrDefault()?.Id;
            var replyId = lastAddedReplyId + 1 ?? 1;

            var author = UserService.GetUser(replyViewModel.Author, forumData);
            var authorId = author.Id;
            var content = string.Join("", replyViewModel.Content);
            var reply = new Reply(replyId, content, authorId, postId);

            forumData.AddReply(reply);
            var post = forumData.Posts.FirstOrDefault(p => p.Id == postId);
            post?.AddReply(reply);

            forumData.SaveChanges();
            return true;
        }
    }
}
