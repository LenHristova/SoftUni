namespace Eventures.Common.Attributes.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    public class NotEmptyOrWhiteSpaceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
            => !string.IsNullOrEmpty(value?.ToString());

        public override string FormatErrorMessage(string name)
            => $"{name} cannot be empty or white space.";
    }
}
