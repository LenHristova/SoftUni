namespace SoftUniCopy.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;

    public class UserDetailsModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
