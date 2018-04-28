using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class SignUpMenuCommand : MenuCommand
    {
        public SignUpMenuCommand(IMenuFactory menuFactory) : base(menuFactory)
        {
        }
    }
}
