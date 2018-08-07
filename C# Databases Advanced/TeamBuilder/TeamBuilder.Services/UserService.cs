namespace TeamBuilder.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;

    public class UserService : IUserService
    {
        private readonly TeamBuilderContext context;
        private readonly ISession session;

        public UserService(TeamBuilderContext context, ISession session)
        {
            this.context = context;
            this.session = session;
        }

        public void RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool Exists(string username)
            => this.ByUsername(username) != null;

        public User ByUsername(string username)
            => this.context.Users
                .SingleOrDefault(t => t.Username == username);

        public User GetCurrentUser()
            => this.session.GetCurrentUser();

        public bool IsAuthenticated()
            => this.session.IsAuthenticated();

        public void Login(User user)
            => this.session.Login(user);

        public void Logout()
            => this.session.Logout();

        public User DeleteUser()
        {
            var loggedInUser = this.session.GetCurrentUser();

            loggedInUser.IsDeleted = true;

            this.context.SaveChanges();

            this.session.Logout();

            return loggedInUser;
        }

        public User GetUserByCredentials(string username, string password)
            => this.context.Users
                .SingleOrDefault(u => u.Username == username &&
                                      u.IsDeleted == false &&
                                      u.Password == password);


            
        
    }
}
