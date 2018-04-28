using Forum.DataModels;

namespace Forum.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using ViewModels;
    using Data;

    public class PostService : IPostService
    {
        private readonly ForumData forumData;
        private readonly IUserService userService;

        public PostService(ForumData forumData, IUserService userService)
        {
            this.forumData = forumData;
            this.userService = userService;
        }

        public IEnumerable<ICategoryInfoViewModel> GetAllCategories()
        {
            IEnumerable<ICategoryInfoViewModel> categories = this.forumData
                .Categories
                .Select(c => new CategoryInfoViewModel(c.Id, c.Name, c.Posts.Count));

            return categories;
        }

        public IEnumerable<IPostInfoViewModel> GetCategoryPostsInfo(int categoryId)
        {
            Category category = this.GetCategoryById(categoryId);
            IEnumerable<IPostInfoViewModel> posts = category
                .Posts
                .Select(this.GetPostById)
                .Select(p => new PostInfoViewModel(p.Id, p.Title, p.Replies.Count));

            return posts;
        }

        private Category GetCategoryById(int categoryId)
        {
            Category category = this.forumData
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new InvalidOperationException("Category not found!");
            }

            return category;
        }

        public string GetCategoryName(int categoryId)
        {
            Category category = this.GetCategoryById(categoryId);
            return category.Name;
        }

        public IPostViewModel GetPostViewModel(int postId)
        {
            Post post = this.GetPostById(postId);

            string postAuthorName = this.userService.GetUserName(post.AuthorId);

            IPostViewModel postView = new PostViewModel(post.Title, postAuthorName, post.Content, this.GetPostReplies(postId));

            return postView;
        }

        private IEnumerable<IReplyViewModel> GetPostReplies(int postId)
        {
            return this.forumData
                .Replies
                .Where(r => r.PostId == postId)
                .Select(r => new ReplyViewModel(this.userService.GetUserName(r.AuthorId), r.Content));
        }

        private Post GetPostById(int postId)
        {
            Post post = this.forumData
                .Posts
                .FirstOrDefault(p => p.Id == postId);

            if (post == null)
            {
                throw new InvalidOperationException("Post not found!");
            }

            return post;
        }

        public int AddPost(int userId, string postTitle, string postCategory, string postContent)
        {
            bool emptyTitle = string.IsNullOrWhiteSpace(postTitle);
            bool emptyCategory = string.IsNullOrWhiteSpace(postCategory);
            bool emptyContent = string.IsNullOrWhiteSpace(postContent);

            if (emptyTitle || emptyCategory || emptyContent)
            {
                throw new ArgumentException("All fields must be filled!");
            }

            int postId = this.forumData.Posts.LastOrDefault()?.Id + 1 ?? 1;
            Category category = this.EnsureCategory(postCategory);
            Post post = new Post(postId, postTitle, postContent, category.Id, userId, new List<int>());

            this.forumData.Posts.Add(post);

            User author = this.userService.GetUserById(userId);
            author.Posts.Add(post.Id);
            category.Posts.Add(post.Id);
            this.forumData.SaveChanges();

            return post.Id;
        }

        private Category EnsureCategory(string categoryName)
        {
            Category category = this.forumData
                .Categories
                .FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                int categoryId = this.forumData.Categories.LastOrDefault()?.Id + 1 ?? 1;

                category = new Category(categoryId, categoryName, new List<int>());
                this.forumData.Categories.Add(category);
            }

            return category;
        }

        public void AddReplyToPost(int postId, string replyContents, int userId)
        {
            int replyId = this.forumData.Posts.LastOrDefault()?.Id + 1 ?? 1;
            Reply reply = new Reply(replyId, replyContents, userId, postId);
            this.forumData.Replies.Add(reply);

            Post post = this.GetPostById(postId);
            post.Replies.Add(replyId);

            this.forumData.SaveChanges();
        }
    }
}
