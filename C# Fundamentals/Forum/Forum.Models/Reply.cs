namespace Forum.Models
{
    using Forum.Models.Contracts;

    public class Reply : Identifiable, IReply
    {
        public Reply(int id, string content, int authorId, int postId) 
            : base(id)
        {
            Content = content;
            AuthorId = authorId;
            PostId = postId;
        }

        public string Content { get; }

        public int AuthorId { get; }

        public int PostId { get; }
    }
}
