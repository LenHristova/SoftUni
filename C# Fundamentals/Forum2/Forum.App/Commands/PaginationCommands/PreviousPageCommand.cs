using Forum.App.Contracts;

namespace Forum.App.Commands.PaginationCommands
{
    public class PreviousPageCommand : PaginationCommand
    {
        public PreviousPageCommand(ISession session) : base(session)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            IMenu menu = this.session.CurrentMenu;
            if (menu is IPaginatedMenu paginatedMenu)
            {
                paginatedMenu.ChangePage(false);
            }

            return menu;
        }
    }
}
