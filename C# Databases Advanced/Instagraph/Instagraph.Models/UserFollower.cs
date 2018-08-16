namespace Instagraph.Models
{
    public class UserFollower
    {
        //•	UserId – an integer, Primary Key
        //•	User – a User
        //•	FollowerId – an integer, Primary Key
        //•	Follower – a User

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int FollowerId { get; set; }
        public virtual User Follower { get; set; }
    }
}
