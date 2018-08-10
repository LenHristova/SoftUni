namespace PetClinic.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phone = value.ToString();
            var validStyle1 = phone.Length == 13 && 
                              phone.StartsWith("+359") && 
                              phone.Skip(1).All(char.IsDigit);

            var validStyle2 = phone.Length == 10 && 
                              phone.StartsWith("0") && 
                              phone.All(char.IsDigit);

            return validStyle1 || validStyle2;
        }
    }
}
