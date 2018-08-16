namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {        
        //•	Id – an integer, Primary Key
        //•	Caption – a string
        //•	UserId – an integer
        //•	User – a User
        //•	PictureId – an integer
        //•	Picture – a Picture
        //•	Comments – a Collection of type Comment
        
        public int Id { get; set; }

        [Required]
        public string Caption { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
