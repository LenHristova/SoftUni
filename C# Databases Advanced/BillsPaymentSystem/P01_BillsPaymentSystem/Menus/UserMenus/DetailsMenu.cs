namespace P01_BillsPaymentSystem.Menus.UserMenus
{
    using Attributes;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    [SubUserMenuAttribute]
    public class DetailsMenu : Menu
    {
        public DetailsMenu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel)
            : base(reader, writer, mainController, userViewModel)
        { }

        public override IMenu ExecutePartial()
        {
            this.writer.WriteLine(this.userViewModel.GetDetailedViewToString());

            var menu = this.reader.ReadLine();

            return this.mainController.OpenMenu(menu);
        }
    }
}
