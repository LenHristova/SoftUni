namespace IRunes.Web.Controllers
{
    using Services;
    using Services.Contracts;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly IUserService users;
        private readonly IHashService hashService;

        public AccountController(IHttpRequest httpRequest)
            : base(httpRequest)
        {
            this.users = new UserService();
            this.hashService = new HashService();
        }

        public IHttpResponse Register() 
            => this.IsAuthenticated ? new RedirectResult("/") : this.View();

        public IHttpResponse Register(RegisterUserViewModel model)
        {
            if (this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            if (!this.IsValid(model, out var results))
            {
                this.AddErrorMessageToViewData(string.Join("<br />", results));

                return this.View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                this.AddErrorMessageToViewData("Passwords do not match.");

                return this.View();
            }

            var userExists = this.users.Exists(model.Username);

            if (userExists)
            {
                this.AddErrorMessageToViewData("This username is taken.");

                return this.View();
            }

            var passwordHash = this.hashService.ComputeHash(model.Password);

            var successfulRegistration = this.users.Create(model.Username, model.Email, passwordHash);

            if (!successfulRegistration)
            {
                return this.ServerError("Oops.. something happened...");
            }

            this.SignInUser(model.Username);

            return new RedirectResult("/");
        }

        public IHttpResponse Login() 
            => this.IsAuthenticated ? new RedirectResult("/") : this.View();

        public IHttpResponse Login(LoginUserViewModel model)
        {
            if (this.IsAuthenticated)
            {
                return new RedirectResult("/");
            }

            if (!this.IsValid(model, out var results))
            {
                this.AddErrorMessageToViewData(string.Join("<br />", results));

                return this.View();
            }

            var passwordHash = this.hashService.ComputeHash(model.Password);

            var isEmailLogin = model.UsernameOrEmail.Contains("@");

            var userCredentialsExists = isEmailLogin
                ? this.users.FindByEmail(model.UsernameOrEmail, passwordHash)
                : this.users.FindByUsername(model.UsernameOrEmail, passwordHash);

            if (!userCredentialsExists)
            {
                this.AddErrorMessageToViewData("Invalid username or password.");

                return this.View();
            }

            var username = isEmailLogin
                ? this.users.GetUsername(model.UsernameOrEmail, passwordHash)
                : model.UsernameOrEmail;

            this.SignInUser(username);

            return new RedirectResult("/");
        }

        public IHttpResponse Logout()
        {
            this.HttpRequest.Session.Clear();
            return new RedirectResult("/");
        }
    }
}
