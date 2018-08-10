namespace PetClinic.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SerialNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var serialNumber = value.ToString();
            var hasRightSize = serialNumber.Length == 10;

            if (!hasRightSize)
            {
                return false;
            }

            var startWithSevenLetters = serialNumber
                .Take(7)
                .All(char.IsLetter);

            var endWithThreeDigits = serialNumber
                .Skip(7)
                .All(char.IsDigit);

            return startWithSevenLetters && endWithThreeDigits;
        }
    }
}
