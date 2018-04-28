using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class ViewPostMenuCommand : MenuCommand
    {
        public ViewPostMenuCommand(IMenuFactory menuFactory) 
            : base(menuFactory)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            IMenu menu = base.Execute();

            if (menu is IIdHoldingMenu holdingMenu)
            {
                int categoryId = int.Parse(args[0]);
                holdingMenu.SetId(categoryId);
            }

            return menu;
        }
    }
}