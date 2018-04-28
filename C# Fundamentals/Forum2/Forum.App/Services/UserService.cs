using System;
using System.Linq;

using Forum.App.Contracts;
using Forum.Data;
using Forum.DataModels;

namespace Forum.App.Services
{
    public class UserService : IUserService
    {
        private readonly ForumData forumData;
        private readonly ISession session;

        public UserService(ForumData forumData, ISession session)
        {
            this.forumData = forumData;
            this.session = session;
        }

        public bool TrySignUpUser(string username, string password)
        {
            const int minLength = 3;
            bool validUsername = !string.IsNullOrWhiteSpace(username) && username.Length > minLength;
            bool validPassword = !string.IsNullOrWhiteSpace(password) && password.Length > minLength;
            if (!validUsername || !validPassword)
            {
                throw new ArgumentException($"Username and Passwor must be longer then {minLength} symbols!");
            }

            var userExists = this.forumData.Users
                .Any(u => u.Username == username);

            if (userExists)
            {
                throw new InvalidOperationException("Username taken!");
            }

            var newUserId = this.forumData.Users.LastOrDefault()?.Id + 1 ?? 1;
            var user = new User(newUserId, username, password);

            this.forumData.Users.Add(user);
            this.forumData.SaveChanges();

            this.TryLogInUser(username, password);

            return true;
        }

        public bool TryLogInUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            User user = this.forumData.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return false;
            }

            this.session.Reset();
            this.session.LogIn(user);
            return true;
        }

        public string GetUserName(int userId)
        {
            return this.GetUserById(userId).Username;
        }

        public User GetUserById(int userId)
        {
            User user = this.forumData
                .Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found!");
            }

            return user;
        }
    }
}
