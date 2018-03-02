using System.Linq;

namespace P04_Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string number)
        {
            var isValidNumber = number.All(char.IsDigit) && !string.IsNullOrWhiteSpace(number);
            return isValidNumber ? $"Calling... {number}" : "Invalid number!";
        }

        public string Browse(string site)
        {
            var isValidSite = site.All(ch => !char.IsDigit(ch));
            return isValidSite ? $"Browsing: {site}!" : "Invalid URL!";
        }
    }
}