using System.Collections.Generic;
using System.Linq;

namespace Forum.App.Models
{
    using Contracts;
    using DataModels;

    public class Session : ISession
    {
        private User user;
        private readonly Stack<IMenu> history;

        public Session()
        {
            this.history = new Stack<IMenu>();
        }

        public string Username => this.user?.Username;

        public int UserId => this.user?.Id ?? 0;

        public bool IsLoggedIn => this.user != null;

        public IMenu CurrentMenu => this.history.Peek();

        public IMenu Back()
        {
            if (this.history.Count > 1)
            {
                this.history.Pop();
            }

            this.CurrentMenu.Open();

            return this.CurrentMenu;
        }

        public void LogIn(User user) => this.user = user;

        public void LogOut() => this.user = null;

        public bool PushView(IMenu view)
        {
            if (this.history.Any() && this.history.Peek() == view)
            {
                return false;
            }

            this.history.Push(view);
            return true;
        }

        public void Reset()
        {
            this.history.Clear();
            this.user = null;
        }
    }
}

