namespace Forum.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Forum.Models;
    using Forum.Models.Contracts;

    public class DataMapper
    {
        private const string DATA_PATH = "../data/";
        private const string CONFIG_PATH = "config.ini";
        private const string USERS = "users";
        private const string CATEGORIES = "categories";
        private const string POSTS = "posts";
        private const string REPLIES = "replies";

        private static readonly string defaultConfig = 
            $"{USERS}={USERS}.csv{Environment.NewLine}" +
            $"{CATEGORIES}={CATEGORIES}.csv{Environment.NewLine}" +
            $"{POSTS}={POSTS}.csv{Environment.NewLine}" +
            $"{REPLIES}={REPLIES}.csv";

        private static readonly IDictionary<string, string> config;

        static DataMapper()
        {
            Directory.CreateDirectory(DATA_PATH);
            config = LoadConfig(DATA_PATH + CONFIG_PATH);
        }

        private static void EnsureConfigFile(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                File.WriteAllText(configFilePath, defaultConfig);
            }
        }

        private static void EnsureFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        private static IDictionary<string, string> LoadConfig(string configFilePath)
        {
            EnsureConfigFile(configFilePath);

            var contents = ReadLines(configFilePath);

            return contents
                .Select(l => l.Split('='))
                .ToDictionary(t => t[0], t => DATA_PATH + t[1]);
        }

        private static IEnumerable<string> ReadLines(string path)
        {
            EnsureFile(path);

            var lines = File.ReadAllLines(path);
            return lines;
        }
        
        private static void WriteLines(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines.ToArray());
        }
        
        public static ICollection<IUser> LoadUsers()
        {
            var users = new List<IUser>();
            var dataLines = ReadLines(config[USERS]);

            foreach (var line in dataLines)
            {
                var userArgs = line.Split(';');

                var id = int.Parse(userArgs[0]);
                var username = userArgs[1];
                var password = userArgs[2];
                var postsIds = userArgs[3]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                var user = new User(id, username, password, postsIds);
                users.Add(user);
            }

            return users;
        }
        
        public static void SaveUsers(IEnumerable<IUser> users)
        {
            var lines = new List<string>();

            foreach (var user in users)
            {
                const string categoryFormat = "{0};{1};{2};{3}";
                var line = string.Format(categoryFormat,
                    user.Id,
                    user.Username,
                    user.Password,
                    string.Join(",", user.PostsIds)
                );

                lines.Add(line);
            }

            WriteLines(config[USERS], lines);
        }
        
        public static ICollection<ICategory> LoadCategories()
        {
            var categories = new List<ICategory>();
            var dataLines = ReadLines(config[CATEGORIES]);
            foreach (var line in dataLines)
            {
                var dataArgs = line.Split(';');
                var id = int.Parse(dataArgs[0]);
                var name = dataArgs[1];
                var postsId = dataArgs[2]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                var category = new Category(id, name, postsId);
                categories.Add(category);
            }

            return categories;
        }
        
        public static void SaveCategories(IEnumerable<ICategory> categories)
        {
            var lines = new List<string>();

            foreach (var category in categories)
            {
                const string categoryFormat = "{0};{1};{2}";
                var line = string.Format(categoryFormat,
                    category.Id,
                    category.Name,
                    string.Join(",", category.PostsIds)
                    );

                lines.Add(line);
            }

            WriteLines(config[CATEGORIES], lines);
        }
        
        public static ICollection<IPost> LoadPosts()
        {
            var posts = new List<IPost>();
            var dataLines = ReadLines(config[POSTS]);

            foreach (var line in dataLines)
            {
                var postArgs = line.Split(';');

                var id = int.Parse(postArgs[0]);
                var title = postArgs[1];
                var content = postArgs[2];
                var categoryId = int.Parse(postArgs[3]);
                var authorId = int.Parse(postArgs[4]);
                var replyIds = postArgs[5]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                var post = new Post(id, title, content, categoryId, authorId, replyIds);
                posts.Add(post);
            }

            return posts;
        }
        
        public static void SavePosts(IEnumerable<IPost> posts)
        {
            var lines = new List<string>();

            foreach (var post in posts)
            {
                const string categoryFormat = "{0};{1};{2};{3};{4};{5}";
                var line = string.Format(categoryFormat,
                    post.Id,
                    post.Title,
                    post.Content,
                    post.CategoryId,
                    post.AuthorId,
                    string.Join(", ", post.ReplyIds)
                );

                lines.Add(line);
            }

            WriteLines(config[POSTS], lines);
        }
        
        public static ICollection<IReply> LoadReplies()
        {
            var replies = new List<IReply>();
            var dataLines = ReadLines(config[REPLIES]);

            foreach (var line in dataLines)
            {
                var replyArgs = line.Split(';');

                var id = int.Parse(replyArgs[0]);
                var content = replyArgs[1];
                var authorId = int.Parse(replyArgs[2]);
                var postId = int.Parse(replyArgs[3]);

                var reply = new Reply(id, content, authorId, postId);
                replies.Add(reply);
            }

            return replies;
        }
        
        public static void SaveReplies(IEnumerable<IReply> replies)
        {
            var lines = new List<string>();

            foreach (var reply in replies)
            {
                const string categoryFormat = "{0};{1};{2};{3}";
                var line = string.Format(categoryFormat,
                    reply.Id,
                    reply.Content,
                    reply.AuthorId,
                    reply.PostId
                );

                lines.Add(line);
            }

            WriteLines(config[REPLIES], lines);
        }
    }
}
