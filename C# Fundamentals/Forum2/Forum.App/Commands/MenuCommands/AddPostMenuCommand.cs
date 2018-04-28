using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class AddPostMenuCommand : MenuCommand
    {
        public AddPostMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}