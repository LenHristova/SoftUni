using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class AddReplyCommand : MenuCommand
    {
        public AddReplyCommand(IMenuFactory menuFactory)
            : base(menuFactory)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            string commandName = this.GetType().Name;
            int menuNameLength = commandName.Length - "Command".Length;
            string menuName = commandName.Substring(0, menuNameLength) + "Menu";

            IIdHoldingMenu menu = (IIdHoldingMenu)this.menuFactory.CreateMenu(menuName);
            int postId = int.Parse(args[0]);
            menu.SetId(postId);
            return menu;
        }
    }
}