namespace Forum.App.UserInterface.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Forum.App.Services;
    using Forum.Models.Contracts;

    public class ReplyViewModel
    {
        private const int LINE_LENGHT = 37;

        public ReplyViewModel()
        {
            Content = new List<string>();
        }

        public ReplyViewModel(IReply replay)
        {
            this.Author = UserService.GetUser(replay.AuthorId).Username;
            this.Content = this.GetLines(replay.Content);
        }

        public string Author { get; set; }

        public IList<string> Content { get; set; }

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