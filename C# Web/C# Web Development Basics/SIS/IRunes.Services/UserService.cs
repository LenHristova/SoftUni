namespace IRunes.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;

    public class UserService : IUserService
    {
        public bool Create(string username, string email, string passwordHash)
        {
            using (var db = new IRunesDbContext())
            {
                var user = new User
                {
                    Username = username,
                    PasswordHash = passwordHash,
                    Email = email
                };

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
        }

        public bool Exists(string username)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        public bool FindByUsername(string username, string passwordHash)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Users
                    .Any(u => u.Username == username &&
                              u.PasswordHash == passwordHash);
            }
        }

        public bool FindByEmail(string email, string passwordHash)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Users
                    .Any(u => u.Email == email &&
                              u.PasswordHash == passwordHash);
            }
        }

        public string GetUsername(string email, string passwordHash)
        {
            using (var db = new IRunesDbContext())
            {
                return db.Users
                    .FirstOrDefault(u => u.Email == email &&
                              u.PasswordHash == passwordHash)?.Username;
            }
        }

        //public ProfileViewModel Profile(string username)
        //{
        //    using (var db = new ByTheCakeContext())
        //    {
        //        return db.Users
        //            .Where(u => u.Username == username)
        //            .Select(u => new ProfileViewModel
        //            {
        //                Username = u.Username,
        //                RegistrationDate = u.RegistrationDate,
        //                OrdersCount = u.Orders.Count
        //            })
        //            .FirstOrDefault();
        //    }
        //}

        //public int? GetId(string username)
        //{
        //    using (var db = new ByTheCakeContext())
        //    {
        //        return db.Users
        //            .SingleOrDefault(u => u.Username == username)?.Id;
        //    }
        //}
    }
}
