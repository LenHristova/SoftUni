namespace CameraBazar.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public ICollection<Camera> Cameras { get; set; } = new List<Camera>();
    }
}
