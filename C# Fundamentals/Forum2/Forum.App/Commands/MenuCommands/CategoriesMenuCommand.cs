using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class CategoriesMenuCommand : MenuCommand
    {
        public CategoriesMenuCommand(IMenuFactory menuFactory) 
            : base(menuFactory)
        {
        }
    }
}