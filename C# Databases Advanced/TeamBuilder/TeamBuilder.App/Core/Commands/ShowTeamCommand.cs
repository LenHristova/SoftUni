namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Text;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class ShowTeamCommand : ICommand
    {
        private readonly ITeamService teamService;

        public ShowTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        //	ShowTeam <teamName>
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);

            var teamName = args[0];

            var isExists = this.teamService.Exists(teamName);
            if (!isExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            return this.GetTeamInfo(teamName);
        }

        private string GetTeamInfo(string teamName)
        {
            var team = this.teamService.ByName(teamName);

            var sb = new StringBuilder();
            sb.AppendLine($"{team.Name} {team.Acronym}")
                .AppendLine("Members:");

            foreach (var member in team.Members)
            {
                sb.AppendLine($"  -{member.User.Username}");
            }

            return sb.ToString().Trim();
        }
    }
}
