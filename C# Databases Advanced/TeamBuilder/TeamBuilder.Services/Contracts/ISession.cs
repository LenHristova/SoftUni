namespace TeamBuilder.Services.Contracts
{
    using Models;

    public interface ISession
    {
        User GetCurrentUser();

        bool IsAuthenticated();

        void Login(User user);

        void Logout();
    }
}