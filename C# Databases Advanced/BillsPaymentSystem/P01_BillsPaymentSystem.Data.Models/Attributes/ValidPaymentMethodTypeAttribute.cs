using System.Linq;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class ValidPaymentMethodTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetProperties = validationContext
                .ObjectType
                .GetProperties()
                .Where(p => p.Name == value.ToString() || p.Name == value + "Id")
                .ToList();

            var targetPropertiesValuees = targetProperties
                .Select(p => p.GetValue(validationContext.ObjectInstance))
                .ToList();

            var errMsg = $"Choosen PaymentMethodType is {value} and {targetProperties[0].Name} or {targetProperties[1].Name} mast have value.";

            return targetPropertiesValuees.Any(v => v != null)
                ? ValidationResult.Success
                : new ValidationResult(errMsg);
        }
    }
}
