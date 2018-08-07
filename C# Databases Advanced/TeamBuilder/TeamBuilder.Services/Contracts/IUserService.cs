namespace TeamBuilder.Services.Contracts
{
	using Models;
	using Models.Enums;

    public interface IUserService
    {
        void RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender);

        User GetCurrentUser();

        bool Exists(string username);

        User ByUsername(string username);

        void Login(User user);

        User GetUserByCredentials(string username, string password);

        bool IsAuthenticated();

        void Logout();

        User DeleteUser();
    }
}
