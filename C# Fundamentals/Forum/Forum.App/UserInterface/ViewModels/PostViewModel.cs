namespace Forum.App.UserInterface.ViewModels
{
    using System.Linq;
    using Forum.App.Services;
    using Forum.Models.Contracts;
    using System.Collections.Generic;

    public class PostViewModel
    {
        private const int LINE_LENGHT = 37;

        public PostViewModel()
        {
            Content = new List<string>();
        }

        public PostViewModel(IPost post)
        {
            this.PostId = post.Id;
            this.Title = post.Title;
            this.Author = UserService
                .GetUser(post.AuthorId)
                .Username;
            this.Category = PostService
                .GetCategory(post.CategoryId)
                .Name;
            this.Content = this.GetLines(post.Content);
            this.Replies = PostService
                .GetPostReplies(post.Id);
        }

        public int PostId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public IList<string> Content { get; set; }

        public IList<ReplyViewModel> Replies { get; set; }

        private IList<string> GetLines(string content)
        {
            var contentChars = content.ToCharArray();

            IList<string> lines = new List<string>();

            for (int i = 0; i < content.Length; i += LINE_LENGHT)
            {
                var line = string.Join("", contentChars
                    .Skip(i)
                    .Take(LINE_LENGHT));
                lines.Add(line);
            }

            return lines;
        }
    }
}
