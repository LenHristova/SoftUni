using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class LogInMenuCommand : MenuCommand
    {
        public LogInMenuCommand(IMenuFactory menuFactory) 
            : base(menuFactory)
        {
        }
    }
}
