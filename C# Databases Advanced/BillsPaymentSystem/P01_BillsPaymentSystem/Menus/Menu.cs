namespace P01_BillsPaymentSystem.Menus
{
    using System;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    public abstract class Menu : IMenu
    {
        protected const string BaseNavigationMessage = "If you want to go to the previous menu - just type \"Back\".";

        protected readonly IReader reader;
        protected readonly IWriter writer;
        protected readonly IMainController mainController;
        protected readonly UserViewModel userViewModel;
        protected readonly bool hasLayout;
        protected string errorMessage;
        protected string navigationMessage;

        protected Menu(IReader reader, IWriter writer, IMainController mainController)
        {
            this.reader = reader;
            this.writer = writer;
            this.mainController = mainController;
            this.hasLayout = true;
            this.navigationMessage = BaseNavigationMessage;
        }

        protected Menu(IReader reader, IWriter writer, IMainController mainController, UserViewModel userViewModel) 
        : this(reader, writer, mainController)
        {
            this.userViewModel = userViewModel;
            this.navigationMessage = BaseNavigationMessage;
        }

        public virtual IMenu Execute()
        {
            if (hasLayout)
            {
                this.WriteLayout();
            }

            IMenu menu;
            try
            {
                menu = this.ExecutePartial();
                this.errorMessage = null;
            }
            catch (Exception e)
            {
                menu = this;
                this.errorMessage = e.Message;
            }

            this.writer.Clear();
            return menu;
        }

        private void WriteLayout()
        {
            this.writer.WriteSpecialMessage("BILLS PAYMENT SYSTEM");
            this.writer.WriteSpecialMessage("--------------------");
            this.writer.WriteLine();

            if (this.userViewModel != null)
            {
                this.writer.WriteHelperMessage($"{this.userViewModel.GetBaseViewToString()}");
                this.writer.WriteSpecialMessage("--------------------");
                this.writer.WriteLine();
            }

            if (this.navigationMessage != null)
            {
                this.writer.WriteHelperMessage(this.navigationMessage);
                this.writer.WriteSpecialMessage("--------------------");
                this.writer.WriteLine();
            }

            if (errorMessage != null)
            {
                this.writer.WriteErrorMessage(this.errorMessage);
                this.writer.WriteSpecialMessage("--------------------");
                this.writer.WriteLine();
            }
        }

        public abstract IMenu ExecutePartial();
    }
}
