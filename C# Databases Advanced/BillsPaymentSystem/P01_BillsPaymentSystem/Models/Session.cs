namespace P01_BillsPaymentSystem.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Contracts.Models;
    using ViewModels;

    public class Session : ISession
    {
        private readonly Stack<IMenu> history;

        public Session()
        {
            this.history = new Stack<IMenu>();
        }

        public int UserId { get; protected set; }

        public UserViewModel UserViewModel { get; private set; }

        public IMenu Back()
        {
            if (this.history.Count > 1)
            {
                this.history.Pop();
            }

            return this.history.Peek();
        }

        public void LogIn(int userId, UserViewModel userViewModel)
        {
            this.UserId = userId;
            this.UserViewModel = userViewModel;
        }

        public void RefreshUserViewModel(UserViewModel userViewModel) => 
            this.UserViewModel = userViewModel;
        
        public void LogOut()
        {
            this.Reset();
            this.UserId = 0;
            this.UserViewModel = null;
        }

        public bool PushView(IMenu view)
        {
            if (this.history.Any() && this.history.Peek().GetType() == view.GetType())
            {
                return false;
            }

            this.history.Push(view);
            return true;
        }

        public void Reset() => this.history.Clear();
    }
}

