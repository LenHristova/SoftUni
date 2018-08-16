namespace Instagraph.DataProcessor
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Models;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {

        //•	Picture: "Successfully imported Picture {picturePath}."
        //•	User: "Successfully imported User {username}."
        //•	Post: "Successfully imported Post {postCaption}.”.
        //•	Comment: "Successfully imported Comment {commentContent}."
        //•	User - Follower: "Successfully imported Follower {followerUsername} to User {userUsername}."

        private const string ErrorMessage = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<PictureDto[]>(jsonString);

            var pictures = new List<Picture>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hasDuplicate = pictures.Any(x => x.Path == dto.Path);

                if (hasDuplicate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var picture = Mapper.Map<Picture>(dto);
                pictures.Add(picture);
                sb.AppendLine($"Successfully imported Picture {dto.Path}.");
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //A user must have a valid profile picture, username and password.
        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var users = new List<User>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var picture = context.Pictures.SingleOrDefault(p => p.Path == dto.ProfilePicture);

                if (picture == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hasDuplicate = users.Any(u => u.Username == dto.Username);
                if (hasDuplicate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = new User()
                {
                    Username = dto.Username,
                    Password = dto.Password,
                    ProfilePicture = picture
                };

                users.Add(user);
                sb.AppendLine($"Successfully imported User {dto.Username}.");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //To make someone a follower of another user, both of them must exist in the database.
        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var userFollowers = new List<UserFollower>();
            foreach (var dto in deserialized)
            {
                var user = context.Users.SingleOrDefault(p => p.Username == dto.User);
                var follower = context.Users.SingleOrDefault(p => p.Username == dto.Follower);

                if (user == null || follower == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hasDuplicate = userFollowers.Any(u => u.User.Username == dto.User &&
                                                          u.Follower.Username == dto.Follower);
                if (hasDuplicate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    User = user,
                    Follower = follower
                };

                userFollowers.Add(userFollower);
                sb.AppendLine($"Successfully imported Follower {dto.Follower} to User {dto.User}.");
            }

            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //A post should only be inserted if the user and picture already exist in the database.
        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PostDto[]), new XmlRootAttribute("posts"));
            var deserialized = (PostDto[])serializer.Deserialize(new StringReader(xmlString));

            var posts = new List<Post>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = context.Users.SingleOrDefault(p => p.Username == dto.User);
                var picture = context.Pictures.SingleOrDefault(p => p.Path == dto.Picture);

                if (user == null || picture == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var post = new Post()
                {
                    Caption = dto.Caption,
                    User = user,
                    Picture = picture
                };

                posts.Add(post);

                sb.AppendLine($"Successfully imported Post {dto.Caption}.");
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        //Comments should only be added for existing users and posts.
        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CommentDto[]), new XmlRootAttribute("comments"));
            var deserialized = (CommentDto[])serializer.Deserialize(new StringReader(xmlString));

            var comments = new List<Comment>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = context.Users.SingleOrDefault(p => p.Username == dto.User);
                var post = context.Posts.SingleOrDefault(p => p.Id == dto.Post.Id);

                if (user == null || post == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var comment = new Comment()
                {
                    Content = dto.Content,
                    User = user,
                    Post = post
                };

                comments.Add(comment);

                sb.AppendLine($"Successfully imported Comment {dto.Content}.");
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
            => Validator.TryValidateObject(
                obj,
                new ValidationContext(obj),
                new List<ValidationResult>(),
                true);
    }
}
