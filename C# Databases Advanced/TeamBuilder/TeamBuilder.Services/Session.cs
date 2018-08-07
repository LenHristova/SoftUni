namespace TeamBuilder.Services
{
    using Contracts;
    using Models;

    public class Session : ISession
    {
        private User loggedInUser;

        public void Login(User user) => loggedInUser = user;

        public void Logout() => loggedInUser = null;

        public bool IsAuthenticated() => loggedInUser != null;

        public User GetCurrentUser() => loggedInUser;        
    }
}
