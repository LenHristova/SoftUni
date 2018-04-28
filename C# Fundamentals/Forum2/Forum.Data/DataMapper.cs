namespace Forum.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Forum.DataModels;

    internal static class DataMapper
	{
		private const string DATA_PATH = "../../../../data/";
		private const string CONFIG_PATH = "config.ini";
		private const string DEFAULT_CONFIG = "users=users.csv\r\ncategories=categories.csv\r\nposts=posts.csv\r\nreplies=replies.csv";

		private static readonly Dictionary<string, string> Config;

        static DataMapper()
        {
            Directory.CreateDirectory(DATA_PATH);
            Config = LoadConfig(DATA_PATH + CONFIG_PATH);
        }

		private static Dictionary<string, string> LoadConfig(string configPath)
        {
            EnsureConfigFile(configPath);

            string[] contents = ReadLines(configPath);

            Dictionary<string, string> config = contents
                .Select(l => l.Split('='))
                .ToDictionary(t => t[0], t => DATA_PATH + t[1]);

            return config;
        }
        
        public static List<User> LoadUsers()
		{
			List<User> users = new List<User>();
			string[] dataLines = ReadLines(Config["users"]);

			foreach (string line in dataLines)
			{
				string[] args = line.Split(';');
				int id = int.Parse(args[0]);
				string username = args[1];
				string password = args[2];
				int[] postIds = args[3]
					.Split(',', StringSplitOptions.RemoveEmptyEntries)
					.Select(int.Parse)
					.ToArray();

				User user = new User(id, username, password, postIds);
				users.Add(user);
			}

			return users;
		}

		public static void SaveUsers(List<User> users)
		{
			List<string> lines = new List<string>();

			foreach (User user in users)
			{
				const string userFormat = "{0};{1};{2};{3}";
				string line = string.Format(userFormat,
					user.Id,
					user.Username,
					user.Password,
					string.Join(",", user.Posts)
				);

				lines.Add(line);
			}

			WriteLines(Config["users"], lines.ToArray());
		}

		public static List<Category> LoadCategories()
		{
			List<Category> categories = new List<Category>();
			string[] dataLines = ReadLines(Config["categories"]);

			foreach (string line in dataLines)
			{
				string[] args = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
				int id = int.Parse(args[0]);
				string name = args[1];
                int[] postIds = args[2]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                Category category = new Category(id, name, postIds);
				categories.Add(category);
			}

			return categories;
		}

		public static void SaveCategories(List<Category> categories)
		{
			List<string> lines = new List<string>();

			foreach (Category category in categories)
			{
				const string categoryFormat = "{0};{1};{2}";
				string line = string.Format(categoryFormat,
					category.Id,
					category.Name,
					string.Join(",", category.Posts)
				);

				lines.Add(line);
			}

			WriteLines(Config["categories"], lines.ToArray());
		}

		public static List<Post> LoadPosts()
		{
			List<Post> posts = new List<Post>();
			string[] dataLines = ReadLines(Config["posts"]);

			foreach (string line in dataLines)
			{
				string[] args = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
				int id = int.Parse(args[0]);
				string title = args[1];
				string content = args[2];
				int categoryId = int.Parse(args[3]);
				int authorId = int.Parse(args[4]);
                List<int> replies = new List<int>();
                if (args.Length == 6)
                {
                    replies.AddRange(args[5].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
                }
				Post post = new Post(id, title, content, categoryId, authorId, replies);

				posts.Add(post);
			}

			return posts;
		}

		public static void SavePosts(List<Post> posts)
		{
			List<string> lines = new List<string>();
			foreach (Post post in posts)
			{
				const string postFormat = "{0};{1};{2};{3};{4};{5}";
				string line = string.Format(postFormat,
					post.Id,
					post.Title,
					post.Content,
					post.CategoryId,
					post.AuthorId,
					string.Join(",", post.Replies)
				);
				lines.Add(line);
			}

			WriteLines(Config["posts"], lines.ToArray());
		}

		public static List<Reply> LoadReplies()
		{
			List<Reply> replies = new List<Reply>();

			string[] dataLines = ReadLines(Config["replies"]);

			foreach (string line in dataLines)
			{
				string[] args = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
				int id = int.Parse(args[0]);
				string content = args[1];
				int authorId = int.Parse(args[2]);
				int postId = int.Parse(args[3]);

                replies.Add(new Reply(id, content, authorId, postId));
			}

			return replies;
		}

		public static void SaveReplies(List<Reply> replies)
		{
			List<string> lines = new List<string>();

			foreach (Reply reply in replies)
			{
				const string replyFormat = "{0};{1};{2};{3}";
				string line = string.Format(replyFormat,
					reply.Id,
					reply.Content,
					reply.AuthorId,
					reply.PostId
				);
				lines.Add(line);
			}

			WriteLines(Config["replies"], lines.ToArray());
		}

		private static string[] ReadLines(string path)
		{
			EnsureFile(path);
			string[] lines = File.ReadAllLines(path);
			return lines;
		}
        
        private static void EnsureConfigFile(string configPath)
        {
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, DEFAULT_CONFIG);
            }
        }

        private static void EnsureFile(string path)
		{
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
		}

		private static void WriteLines(string path, string[] lines)
		{
			File.WriteAllLines(path, lines);
		}

	}
}
