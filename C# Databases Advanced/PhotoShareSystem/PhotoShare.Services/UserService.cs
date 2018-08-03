namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Models;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;
        private readonly IMapper mapper;
        private readonly ISession session;

        public UserService(PhotoShareContext context, IMapper mapper, ISession session)
        {
            this.context = context;
            this.mapper = mapper;
            this.session = session;
        }

        public TModel ById<TModel>(int id)
            => By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username)
            => By<TModel>(a => a.Username == username).SingleOrDefault();

        public bool Exists(int id)
            => ById<User>(id) != null;

        public bool Exists(string name)
            => ByUsername<User>(name) != null;

        public User Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            var user = this.context.Users.Single(u => u.Username == username);

            user.IsDeleted = true;

            this.context.SaveChanges();

            this.Logout();
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var frindship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(frindship);
            this.context.SaveChanges();

            return frindship;
        }

        public Friendship AcceptFriend(int userId, int friendId)
            => AddFriend(userId, friendId);

        public void ChangePassword(int userId, string password)
        {
            var user = this.context.Users.Find(userId);

            user.Password = password;

            this.context.SaveChanges();
        }

        public void SetBornTown(int userId, int townId)
        {
            var user = this.context.Users.Find(userId);

            user.BornTownId = townId;

            this.context.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            var user = this.context.Users.Find(userId);

            user.CurrentTownId = townId;

            this.context.SaveChanges();
        }

        public bool IsLogIn() => this.session.IsLogIn();

        public bool IsLogIn(string username) => this.session.IsLogIn(username);

        public void Login(string username)
        {
            var user = this.ByUsername<User>(username);
            this.session.LogIn(user);
        }

        public void Logout() => this.session.LogOut();

        public TEntity LoggedInUser<TEntity>() => this.mapper.Map<TEntity>(this.session.User);

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
            => this.context.Users
                .Where(predicate)
                .AsQueryable()
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();
    }
}