using System.Collections.Generic;
using System.Linq;

namespace Forum.App.ViewModels
{
    public abstract class ContentViewModel
    {
        private const int LINE_LENGTH = 37;

        protected ContentViewModel(string content)
        {
            Content = this.GetLines(content);
        }

        public string[] Content { get; }

        private string[] GetLines(string content)
        {
            char[] contentChars = content.ToCharArray();

            ICollection<string> lines = new List<string>();

            for (int i = 0; i < content.Length; i += LINE_LENGTH)
            {
                char[] row = contentChars
                    .Skip(i)
                    .Take(LINE_LENGTH)
                    .ToArray();

                string line = string.Join("", row);

                lines.Add(line);
            }

            return lines.ToArray();
        }
    }
}
