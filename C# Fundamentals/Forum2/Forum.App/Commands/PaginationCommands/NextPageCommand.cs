using Forum.App.Contracts;

namespace Forum.App.Commands.PaginationCommands
{
    public class NextPageCommand : PaginationCommand
    {
        public NextPageCommand(ISession session) : base(session)
        {
        }

        public override IMenu Execute(params string[] args)
        {
            IMenu menu = this.session.CurrentMenu;
            if (menu is IPaginatedMenu paginatedMenu)
            {
                paginatedMenu.ChangePage();
            }

            return menu;
        }
    }
}
