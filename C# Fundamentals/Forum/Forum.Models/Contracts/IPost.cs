using System.Collections.Generic;

namespace Forum.Models.Contracts
{
    public interface IPost : IIdentifiable
    {
        string Title { get; }
        string Content { get; }
        int CategoryId { get; }
        int AuthorId { get; }
        ICollection<int> ReplyIds { get; }

        void AddReply(IReply reply);
    }
}