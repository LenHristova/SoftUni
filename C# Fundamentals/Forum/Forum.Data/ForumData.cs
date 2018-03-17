namespace Forum.Data
{
    using System.Collections.Generic;
    using Forum.Models.Contracts;

    public class ForumData
    {
        public ForumData()
        {
            Users = DataMapper.LoadUsers();
            Categories = DataMapper.LoadCategories();
            Posts = DataMapper.LoadPosts();
            Replies = DataMapper.LoadReplies();
        }

        public ICollection<IUser> Users { get; }

        public ICollection<ICategory> Categories { get; }

        public ICollection<IPost> Posts { get; }

        public ICollection<IReply> Replies { get; }

        public void AddUser(IUser user)
        {
            this.Users.Add(user);
        }

        public void AddCategory(ICategory category)
        {
            this.Categories.Add(category);
        }

        public void AddPost(IPost post)
        {
           this.Posts.Add(post);
        }

        public void AddReply(IReply reply)
        {
            this.Replies.Add(reply);
        }

        public void SaveChanges()
        {
            DataMapper.SaveUsers(this.Users);
            DataMapper.SaveCategories(this.Categories);
            DataMapper.SavePosts(this.Posts);
            DataMapper.SaveReplies(this.Replies);
        }
    }
}
