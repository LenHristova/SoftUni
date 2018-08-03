namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class AddTagCommand : ICommand
    {
        private readonly ITagService tagService;
        private readonly IUserService userService;

        public AddTagCommand(ITagService tagService, IUserService userService)
        {
            this.tagService = tagService;
            this.userService = userService;
        }

        // AddTag <tag>
        public string Execute(string[] data)
        {
            var isLogIn = this.userService.IsLogIn();
            if (!isLogIn)
            {
                throw new InvalidOperationException("Log in first!");
            }

            if (data.Length != 1)
            {
                throw new InvalidOperationException("Invalid parameters count!");
            }

            var tagName = data[0].ValidateOrTransform();

            var exists = this.tagService.Exists(tagName);

            if (exists)
            {
                throw new ArgumentException($"Tag {tagName} exists!");
            }

            this.tagService.AddTag(tagName);

            return $"Tag {tagName} was added successfully!";
        }
    }
}
