using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class User
{
    public string Name { get; set; }
    public List<DateTime> Dates { get; set; }
    public List<string> Comments { get; set; } = new List<string>();

}
class StartUp
{
    static void Main()
    {
        List<User> users = GetUsers();
        AddComments(users);

        foreach (var user in users.OrderBy(user => user.Name))
        {
            Console.WriteLine(user.Name);
            Console.WriteLine("Comments:");
            foreach (var comment in user.Comments)
            {
                Console.WriteLine($"- {comment}");
            }
            Console.WriteLine("Dates attended:");

            foreach (var date in user.Dates.OrderBy(date => date))
            {
                Console.WriteLine($"-- {date:dd/MM/yyyy}");
            }
        }
    }

    private static void AddComments(List<User> users)
    {
        string input = Console.ReadLine();
        while (input != "end of comments")
        {
            string[] userCommentsInfo = input
                .Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
            string username = userCommentsInfo[0];
            string comment = userCommentsInfo[1];

            if (users.Any(user => user.Name == username))
            {
                users.Find(user => user.Name == username).Comments
                    .Add(comment);
            }

            input = Console.ReadLine();
        }
    }

    private static List<User> GetUsers()
    {
        List<User> users = new List<User>();

        string input = Console.ReadLine();
        while (input != "end of dates")
        {
            string[] userDatesInfo = input
                .Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);

            string username = userDatesInfo[0];
            List<DateTime> userDates = userDatesInfo
                .Skip(1)
                .Select(date => DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                .ToList();

            if (users.Any(user => user.Name == username))
            {
                users.Find(user => user.Name == username).Dates
                    .AddRange(userDates);
            }
            else
            {
                users.Add(new User
                {
                    Name = username,
                    Dates = new List<DateTime>(userDates)
                });

            }

            input = Console.ReadLine();
        }

        return users;
    }
}