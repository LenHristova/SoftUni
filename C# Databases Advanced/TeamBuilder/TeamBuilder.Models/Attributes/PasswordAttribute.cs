namespace TeamBuilder.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
	using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        public int Digits { get; set; }

        public int UppercaseLetters { get; set; }

        public override bool IsValid(object value)
        {
            var hasDigits = value.ToString().Where(char.IsUpper).Count() >= this.Digits;
            var hasUppercaseLetters = value.ToString().Where(char.IsUpper).Count() >= this.UppercaseLetters;

            return hasDigits && hasUppercaseLetters;
        }
    }
}
