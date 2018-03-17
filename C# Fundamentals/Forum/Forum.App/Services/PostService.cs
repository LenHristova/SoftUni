namespace Forum.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Forum.App.UserInterface.ViewModels;
    using Forum.Models.Contracts;
    using Forum.Data;
    using Forum.Models;

    public class PostService
    {
        public static ICategory GetCategory(int categoryId)
        {
            var forumData = new ForumData();

            var searchedCategory = forumData.Categories
                .FirstOrDefault(c => c.Id == categoryId);

            return searchedCategory;
        }

        public static IList<ReplyViewModel> GetPostReplies(int postId)
        {
            var forumData = new ForumData();

            var searchedPost = forumData.Posts
                .FirstOrDefault(p => p.Id == postId);

            var replies = searchedPost?.ReplyIds
                .Select(replyId => GetReply(replyId, forumData))
                .Select(r => new ReplyViewModel(r))
                .ToList();

            return replies;
        }

        private static IReply GetReply(int replyId, ForumData forumData)
        {
            return forumData.Replies.FirstOrDefault(r => r.Id == replyId);
        }

        public static IEnumerable<string> GetAllGategoryNames()
        {
            var forumData = new ForumData();

            var allCategories = forumData
                .Categories
                .Select(c => c.Name);

            return allCategories;
        }

        public static IEnumerable<IPost> GetPostsByCategory(int categoryId)
        {
            var forumData = new ForumData();

            var postsByCategory = forumData
                .Categories.FirstOrDefault(c => c.Id == categoryId)?
                .PostsIds
                .Select(postId => GetPost(postId, forumData));

            return postsByCategory;
        }

        private static IPost GetPost(int postId, ForumData forumData)
        {
            return forumData.Posts.FirstOrDefault(p => p.Id == postId);
        }

        public static PostViewModel GetPostViewModel(int postId)
        {
            var forumData = new ForumData();

            var post = forumData.Posts.FirstOrDefault(p => p.Id == postId);

            return new PostViewModel(post);
        }

        private static ICategory EnsureCategory(PostViewModel postViewModel, ForumData forumData)
        {
            var categoryName = postViewModel.Category;
            var category = forumData.Categories
                .FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                var lastAddedCategoryId = forumData.Categories.LastOrDefault()?.Id;

                var categoryId = lastAddedCategoryId + 1 ?? 1;
                category = new Category(categoryId, categoryName);
                forumData.AddCategory(category);
            }

            return category;
        }

        public static bool TrySavePost(PostViewModel postViewModel)
        {
            var forumData = new ForumData();

            var isEmptyCategory = string.IsNullOrWhiteSpace(postViewModel.Category);
            var isEmptyTitle = string.IsNullOrWhiteSpace(postViewModel.Title);
            var isEmptyContent = !postViewModel.Content.Any();

            if (isEmptyCategory || isEmptyTitle || isEmptyContent)
            {
                return false;
            }

            var lastAddedPostId = forumData.Posts.LastOrDefault()?.Id;
            var postId = lastAddedPostId + 1 ?? 1;
            var title = postViewModel.Title;
            var category = EnsureCategory(postViewModel, forumData);
            var author = UserService.GetUser(postViewModel.Author, forumData);
            var authorId = author.Id;
            var content = string.Join("", postViewModel.Content);
            var post = new Post(postId, title, content, category.Id, authorId);

            forumData.AddPost(post);
            author.AddPost(post);
            category.AddPost(post);

            forumData.SaveChanges();

            postViewModel.PostId = postId;

            return true;
        }
    }
}
