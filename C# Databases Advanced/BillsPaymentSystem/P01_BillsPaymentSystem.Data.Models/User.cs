using P01_BillsPaymentSystem.Data.Models.Attributes;

namespace P01_BillsPaymentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.PaymentMethods = new HashSet<PaymentMethod>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must have at least 2 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must have at least 2 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 7, ErrorMessage = "Email must have at least 7 characters.")]
        [NonUnicode(ErrorMessage = "Email must be non-unicode")]
        public string Email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Password must have at least 6 characters.")]
        [NonUnicode(ErrorMessage = "Email must be non-unicode")]
        public string Password { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
