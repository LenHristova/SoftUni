using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
}

class Group
{
    public List<Student> Students { get; set; } = new List<Student>();
}
class Town
{
    public string Name { get; set; }
    public Group[] Groups { get; set; }
}
class StartUp
{
    static string input = String.Empty;
    static void Main()
    {
        List<Town> registrationsByTown = ProcessRegistrations();

        int allGroups = registrationsByTown
            .Select(town => town.Groups.Length)
            .Sum();

        Console.WriteLine($"Created {allGroups} groups in {registrationsByTown.Count} towns:");
        foreach (var town in registrationsByTown.OrderBy(t=> t.Name))
        {
            foreach (var group in town.Groups)
            {
                List<string> usersEmails = group.Students.Select(st => st.Email).ToList();
                Console.WriteLine($"{town.Name} => {string.Join(", ", usersEmails)}");
            }           
        }
    }

    private static List<Town> ProcessRegistrations()
    {
        List<Town> townsRegistrations = new List<Town>();
         input = Console.ReadLine();

        while (input != "End")
        {
            string[] townInfo = input.Split(new[] { "=>" },
                StringSplitOptions.RemoveEmptyEntries);
            string townName = townInfo[0].Trim();
            int groupsCapacity = int.Parse(townInfo[1].Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).First());

            Group[] studentsGroups = FormGroups(groupsCapacity);
            townsRegistrations.Add(new Town
            {
                Name = townName,
                Groups = studentsGroups
            });
        }

        return townsRegistrations;
    }

    private static Group[] FormGroups(int groupsCapacity)
    {
        List<Student> allRegisteredStudentsForTown = GetAllRegisteredStudentsForTown();
        int groupCount = (int)Math.Ceiling((double)allRegisteredStudentsForTown.Count / groupsCapacity);
        Group[] studentsGroups = new Group[groupCount]
            .Select(gr => new Group()).ToArray();

        for (int groupNumber = 0; groupNumber < groupCount; groupNumber++)
        {
            for (int currSt = 0; currSt < groupsCapacity; currSt++)
            {
                if (allRegisteredStudentsForTown.Count == 0)
                {
                    break;
                }
                studentsGroups[groupNumber].Students.Add(allRegisteredStudentsForTown[0]);
                allRegisteredStudentsForTown.RemoveAt(0);
            }            
        }

        return studentsGroups;
    }

    private static List<Student> GetAllRegisteredStudentsForTown()
    {
        List<Student> allRegisteredStudentsForTown = new List<Student>();
         input = Console.ReadLine();
        while (input != "End" && !input.Contains("=>"))
        {
            string[] studentInfo = input.Split(new[] { '|' },
                StringSplitOptions.RemoveEmptyEntries);
            string studentName = studentInfo[0].Trim();
            string studentEmail = studentInfo[1].Trim();
            DateTime registrationDate = GetDate(studentInfo[2]);
            allRegisteredStudentsForTown.Add(new Student
            {
                Name = studentName,
                Email = studentEmail,
                Date = registrationDate
            });

            input = Console.ReadLine();
        }
        return allRegisteredStudentsForTown
            .OrderBy(st => st.Date)
            .ThenBy(st => st.Name)
            .ThenBy(st => st.Email)
            .ToList();
    }

    private static DateTime GetDate(string date)
    {
        string[] dateInfo = date.Trim()
            .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        string month = DateTime.ParseExact(dateInfo[1], "MMM", CultureInfo.InvariantCulture).Month.ToString();
        string formatedDate = dateInfo[0] + "-" + month + "-" + dateInfo[2];
        return DateTime.ParseExact(formatedDate.Trim(),
            "d-M-yyyy", CultureInfo.InvariantCulture);
    }
}