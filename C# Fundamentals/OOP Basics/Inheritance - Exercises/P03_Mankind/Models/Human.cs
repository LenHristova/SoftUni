using System;
using System.Linq;

namespace P03_Mankind.Models
{
    public class Human
    {
        private const int FIRST_NAME_MIN_LENGTH = 4;
        private const int LAST_NAME_MIN_LENGTH = 3;

        private string firstName;
        private string lastName;

        protected string FirstName
        {
            get => firstName;
            set
            {
                Validator.ValidateNameFirstLetter(value.FirstOrDefault(), nameof(firstName));
                Validator.ValidateNameLength(value, FIRST_NAME_MIN_LENGTH, nameof(firstName));

                firstName = value;
            }
        }

        protected string LastName
        {
            get => lastName;
            set
            {
                Validator.ValidateNameFirstLetter(value.FirstOrDefault(), nameof(lastName));
                Validator.ValidateNameLength(value, LAST_NAME_MIN_LENGTH, nameof(lastName));

                lastName = value;
            }
        }

        public Human(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"First Name: {FirstName}{Environment.NewLine}" +
                   $"Last Name: {LastName}";
        }
    }
}