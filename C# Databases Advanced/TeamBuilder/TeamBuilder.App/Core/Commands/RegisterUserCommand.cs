namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Models.Enums;
    using Services.Contracts;
    using Utilities;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        //	RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(string[] args)
        {
            Check.CheckLength(7, args);

            if (this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var username = args[0];
            ValidateUsername(username);

            var password = args[1];
            ValidatePassword(password);

            var repeatedPassword = args[2];
            ValidatePasswordsAreEqual(password, repeatedPassword);

            var firstName = args[3];
            ValidateFirstName(firstName);

            var lastName = args[4];
            ValidateLastName(lastName);

            var ageString = args[5];
            var age = ValidateAge(ageString);

            var genderString = args[6];
            var gender = ValidateGender(genderString);

            var userExists = this.userService.Exists(username);
            if (userExists)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.userService.RegisterUser(username, password, firstName, lastName, age, gender);

            return string.Format(Constants.SuccessfulMessages.SuccessfulRegistration, username);
        }

        private static Gender ValidateGender(string genderString)
        {
            var isValidGender = Enum.TryParse<Gender>(genderString, true, out var gender);
            if (!isValidGender)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            return gender;
        }

        private static int ValidateAge(string ageString)
        {
            var isValid = int.TryParse(ageString, out var age);
            if (!isValid || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            return age;
        }

        private static void ValidateLastName(string lastName)
        {
            var isValid = lastName.Length <= Constants.MaxLastNameLength;
            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.LastNameNotValid, lastName));
            }
        }

        private static void ValidateFirstName(string firstName)
        {
            var isValid = firstName.Length <= Constants.MaxFirstNameLength;
            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.FirstNameNotValid, firstName));
            }
        }

        private static void ValidatePasswordsAreEqual(string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }
        }

        private static void ValidatePassword(string password)
        {
            var isValid = password.Length >= Constants.MinPasswordLength &&
                                  password.Length <= Constants.MaxPasswordLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }
        }

        private static void ValidateUsername(string username)
        {
            var isValid = username.Length >= Constants.MinUsernameLength &&
                                  username.Length <= Constants.MaxUsernameLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }
        }
    }
}
