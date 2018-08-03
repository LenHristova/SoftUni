namespace PhotoShare.Services.Contracts
{
	using Models;

    public interface ISession
    {
        User User { get; }

        void LogIn(User user);

        void LogOut();

        bool IsLogIn();

        bool IsLogIn(string username);
    }
}
