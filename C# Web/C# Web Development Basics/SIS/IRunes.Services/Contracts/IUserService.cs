namespace IRunes.Services.Contracts
{
    public interface IUserService
   {
       bool Create(string username, string email, string passwordHash);

       bool Exists(string username);

       bool FindByUsername(string username, string passwordHash);

       bool FindByEmail(string email, string passwordHash);

       string GetUsername(string email, string passwordHash);

       //ProfileViewModel Profile(string username);

       //int? GetId(string username);
   }
}
