namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        //• Id – an integer, Primary Key
        //•	Username – a string with max length 30, Unique
        //•	Password – a string with max length 20
        //•	ProfilePictureId – an integer
        //•	ProfilePicture – a Picture
        //•	Followers – a Collection of type UserFollower
        //•	UsersFollowing – a Collection of type UserFollower
        //•	Posts – a Collection of type Post
        //•	Comments – a Collection of type Comment

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Password { get; set; }

        public int ProfilePictureId { get; set; }
        public virtual Picture ProfilePicture { get; set; }

        public virtual ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

        public virtual ICollection<UserFollower> UsersFollowing { get; set; } = new List<UserFollower>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
