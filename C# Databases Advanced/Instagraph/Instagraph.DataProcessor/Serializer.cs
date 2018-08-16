namespace Instagraph.DataProcessor
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Dtos.Export;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(uncommentPosts, Formatting.Indented);
            return serialized;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users
                .Where(u => u.Posts.Any(p =>
                    p.Comments.Any(c => u.Followers.Any(f => f.FollowerId == c.User.Id))))
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count

                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(users, Formatting.Indented);
            return serialized;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new UserMostCommentsPostDto
                {
                    Username = u.Username,
                    MostComments = u.Posts.Any() 
                        ? u.Posts
                        .Select(p => p.Comments.Any() ? p.Comments.Count : 0)
                        .OrderByDescending(x => x)
                        .FirstOrDefault()
                        : 0
                })
                .OrderByDescending(x => x.MostComments)
                .ThenBy(x => x.Username)
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(UserMostCommentsPostDto[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty}));

            return sb.ToString();
        }
    }
}
