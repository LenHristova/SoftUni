namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class NonUnicodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }


            var errMsg = $"{validationContext.DisplayName} must be non-unicode.";
            foreach (var ch in value.ToString())
            {
                if (ch > 255)
                {
                    return new ValidationResult(errMsg);
                }
            }

            return ValidationResult.Success;
        }
    }
}
