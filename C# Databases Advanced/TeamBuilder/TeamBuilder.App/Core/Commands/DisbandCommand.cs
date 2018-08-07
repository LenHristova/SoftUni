namespace TeamBuilder.App.Core.Commands
{
	using System;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class DisbandCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;

        public DisbandCommand(IUserService userService, ITeamService teamService)
        {
            this.userService = userService;
            this.teamService = teamService;
        }

        //	Disband <teamName>
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var teamName = args[0];

            var teamExists = this.teamService.Exists(teamName);

            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var team = this.teamService.ByName(teamName);
            var loggedInUserId = this.userService.GetCurrentUser().Id;

            var isLoggedInUserCreator = team.CreatorId == loggedInUserId;
            if (!isLoggedInUserCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            this.teamService.Disband(team);

            return string.Format(Constants.SuccessfulMessages.SuccessfulDisbandTeam, teamName);
        }
    }
}
