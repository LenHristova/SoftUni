using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public abstract class MenuCommand : ICommand
    {
        protected readonly IMenuFactory menuFactory;

        protected MenuCommand(IMenuFactory menuFactory)
        {
            this.menuFactory = menuFactory;
        }

        public virtual IMenu Execute(params string[] args)
        {
            string commandName = this.GetType().Name;
            int menuNameLength = commandName.Length - "Command".Length;
            string menuName = commandName.Substring(0, menuNameLength);

            IMenu menu = menuFactory.CreateMenu(menuName);
            return menu;
        }
    }
}