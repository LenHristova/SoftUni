namespace P01_BillsPaymentSystem.Core
{
    using Contracts;
    using Contracts.Core;
    using Menus.MainMenus;

    public class Engine : IEngine
    {
        private readonly IMainController mainController;

        public Engine(IMainController mainController)
        {
            this.mainController = mainController;
        }

        public void Run()
        {
            var menu = mainController.OpenMenu(nameof(MainMenu));

            while (true)
            {
                menu = menu.Execute();
            }
        }
    }
}
