namespace Forum.Models
{
    using System.Collections.Generic;
    using Forum.Models.Contracts;

    public class User : Identifiable, IIdentifiable, IUser
    {
        public User(int id, string username, string password, IEnumerable<int> postsIds = null) 
            : base(id)
        {
            Username = username;
            Password = password;
            PostsIds = postsIds == null 
                ? new List<int>() 
                : new List<int>(postsIds);
        }

        public string Username { get; }

        public string Password { get; }

        public ICollection<int> PostsIds { get; }

        public void AddPost(IPost post)
        {
            this.PostsIds.Add(post.Id);
        }
    }
}