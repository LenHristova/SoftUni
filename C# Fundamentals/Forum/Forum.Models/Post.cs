using System.Collections.Generic;
using Forum.Models.Contracts;

namespace Forum.Models
{
    public class Post : Identifiable, IIdentifiable, IPost
    {
        public Post(int id, string title, string content, int categoryId, int authorId, IEnumerable<int> replyIds = null) 
            : base(id)
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            AuthorId = authorId;
            ReplyIds = replyIds == null
                ? new List<int>()
                : new List<int>(replyIds);
        }

        public string Title { get; }

        public string Content { get; }

        public int CategoryId { get; }

        public int AuthorId { get; }

        public ICollection<int> ReplyIds { get; }

        public void AddReply(IReply reply)
        {
            this.ReplyIds.Add(reply.Id);
        }
    }
}