using Forum.App.Contracts;

namespace Forum.App.Commands.MenuCommands
{
    public class ViewCategoryMenuCommand : MenuCommand
    {
        public ViewCategoryMenuCommand(IMenuFactory menuFactory) 
            : base(menuFactory)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            IIdHoldingMenu menu = (IIdHoldingMenu)base.Execute();

            int categoryId = int.Parse(args[0]);
            menu.SetId(categoryId);
            return menu;
        }
    }
}
