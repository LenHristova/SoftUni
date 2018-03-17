using System.Collections.Generic;

namespace Forum.Models.Contracts
{
    public interface ICategory : IIdentifiable
    {
        string Name { get; }
        ICollection<int> PostsIds { get; }

        void AddPost(IPost post);
    }
}