using System.Collections.Generic;
using Forum.Models.Contracts;

namespace Forum.Models
{
    public class Category : Identifiable, IIdentifiable, ICategory
    {
        public Category(int id, string name, IEnumerable<int> postsIds = null) 
            : base(id)
        {
            Name = name;
            PostsIds = postsIds == null
                ? new List<int>()
                : new List<int>(postsIds);
        }

        public string Name { get; }

        public ICollection<int> PostsIds { get; }

        public void AddPost(IPost post)
        {
            this.PostsIds.Add(post.Id);
        }
    }
}
