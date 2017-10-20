using System;
using System.Collections.Generic;
using System.Linq;

class Team
{
    public string Creator { get; set; }
    public List<string> Members { get; set; } = new List<string>();
}

class StartUp
{
    static void Main()
    {
        Dictionary<string, Team> teams = CreateTeams();
        AddMembers(teams);
        List<string> teamsToDisband = FindZeroMembersTeams(teams);
        teams = DisbandZeroMembersTeams(teams);
        PrintTeams(teams, teamsToDisband);
    }

    private static void PrintTeams(Dictionary<string, Team> teams, List<string> teamsToDisband)
    {
        foreach (var team in teams.OrderByDescending(t => t.Value.Members.Count).ThenBy(t => t.Key))
        {
            Console.WriteLine(team.Key);
            Console.WriteLine($"- {team.Value.Creator}");
            foreach (var member in team.Value.Members.OrderBy(m => m))
            {
                Console.WriteLine($"-- {member}");
            }
        }

        Console.WriteLine("Teams to disband:");
        
        Console.WriteLine(string.Join(Environment.NewLine, teamsToDisband));
    }

    private static List<string> FindZeroMembersTeams(Dictionary<string, Team> teams)
    {
        return teams.Where(team => team.Value.Members.Count == 0)
            .Select(t => t.Key)
            .OrderBy(t => t)
            .ToList();
    }

    private static Dictionary<string, Team> DisbandZeroMembersTeams(Dictionary<string, Team> teams)
    {
        return teams.Where(team => team.Value.Members.Count != 0)
            .ToDictionary(k => k.Key, v => v.Value);
    }

    private static void AddMembers(Dictionary<string, Team> teams)
    {
        string input = Console.ReadLine();
        while (input != "end of assignment")
        {
            string[] memberInfo = input
                .Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

            string memberName = memberInfo[0];
            string teamName = memberInfo[1];

            if (!teams.ContainsKey(teamName))
            {
                Console.WriteLine($"Team {teamName} does not exist!");
            }
            else if (teams.Values.Any(team => team.Creator == memberName) ||
                teams.Values.Any(team => team.Members.Any(m => m == memberName)))
            {
                Console.WriteLine($"Member {memberName} cannot join team {teamName}!");
            }
            else
            {
                teams[teamName].Members.Add(memberName);
            }

            input = Console.ReadLine();
        }
    }

    private static Dictionary<string, Team> CreateTeams()
    {
        Dictionary<string, Team> teams = new Dictionary<string, Team>();
        int teamsCount = int.Parse(Console.ReadLine());

        for (int currTeam = 0; currTeam < teamsCount; currTeam++)
        {
            string[] teamInfo = Console.ReadLine()
                .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            string creator = teamInfo[0];
            string teamName = teamInfo[1];

            if (teams.ContainsKey(teamName))
            {
                Console.WriteLine($"Team {teamName} was already created!");
            }
            else if (teams.Values.Any(v => v.Creator == creator))
            {
                Console.WriteLine($"{creator} cannot create another team!");
            }
            else
            {
                teams.Add(teamName, new Team
                {
                    Creator = creator
                });
                Console.WriteLine($"Team {teamName} has been created by {creator}!");
            }
        }

        return teams;
    }
}