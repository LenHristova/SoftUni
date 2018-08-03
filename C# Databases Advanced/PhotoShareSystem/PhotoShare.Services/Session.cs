namespace PhotoShare.Services
{
	using Contracts;
	using Models;

    public class Session : ISession
    {
        public User User { get; private set; }

        public void LogIn(User user) => this.User = user;

        public void LogOut() => this.User = null;

        public bool IsLogIn() => this.User != null;

        public bool IsLogIn(string username) => this.User != null && this.User.Username == username;
    }
}
