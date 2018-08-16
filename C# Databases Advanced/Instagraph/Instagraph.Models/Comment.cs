namespace Instagraph.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        //•	Id – an integer, Primary Key
        //•	Content – a string with max length 250
        //•	UserId – an integer
        //•	User – a User
        //•	PostId – an integer
        //•	Post – a Post

        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string Content { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
