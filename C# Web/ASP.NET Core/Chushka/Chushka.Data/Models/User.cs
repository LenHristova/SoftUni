namespace Chushka.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
