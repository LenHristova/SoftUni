using System.Collections.Generic;

namespace Forum.Models.Contracts
{
    public interface IUser : IIdentifiable
    {
        string Username { get; }
        string Password { get; }
        ICollection<int> PostsIds { get; }

        void AddPost(IPost post);
    }
}