namespace Forum.App.Services
{
    using System.Linq;
    using Data;
    using Models;
    using Forum.App.Controllers.UserControllers;
    using Forum.Models.Contracts;

    public class UserService
    {
        public static AccountController.ActionStatus TryExecuteAction(string username, string password, string wantedAction)
        {
            switch (wantedAction)
            {
                case "Login":
                    return TryLogInUser(username, password);
                case "Signup":
                    return TrySignUpUser(username, password);
            }

            throw new InvalidCommandException();
        }

        private static AccountController.ActionStatus TryLogInUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return AccountController.ActionStatus.DetailsError;
            }

            var forumData = new ForumData();

            var isUserExists = forumData.Users
                .Any(u => u.Username == username && u.Password == password);

            return !isUserExists
                ? AccountController.ActionStatus.DetailsError
                : AccountController.ActionStatus.Success;
        }

        private static AccountController.ActionStatus TrySignUpUser(string username, string password)
        {
            var isValidUsername = !string.IsNullOrWhiteSpace(username) && username.Length >= 3;
            var isValidPassword = !string.IsNullOrWhiteSpace(username) && username.Length >= 3;

            if (!isValidUsername || !isValidPassword)
            {
                return AccountController.ActionStatus.DetailsError;
            }

            var forumData = new ForumData();

            var isUsernameAlreadyExists = forumData.Users
                .Any(u => u.Username == username);

            if (isUsernameAlreadyExists)
            {
                return AccountController.ActionStatus.UsernameTakenError;
            }

            var lastAddedUserId = forumData.Users.LastOrDefault()?.Id;
            var newUserId = lastAddedUserId + 1 ?? 1;
            var user = new User(newUserId, username, password);

            forumData.AddUser(user);
            forumData.SaveChanges();
            return AccountController.ActionStatus.Success;
        }

        public static IUser GetUser(int userId)
        {
            var forumData = new ForumData();
            var searchedUser = forumData.Users
                .FirstOrDefault(u => u.Id == userId);

            return searchedUser;
        }

        public static IUser GetUser(string username, ForumData forumData)
        {
            var searchedUser = forumData.Users
                .FirstOrDefault(u => u.Username == username);

            return searchedUser;
        }
    }
}
