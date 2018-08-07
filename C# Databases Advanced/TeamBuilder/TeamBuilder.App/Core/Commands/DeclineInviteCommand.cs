namespace TeamBuilder.App.Core.Commands
{
	using System;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class DeclineInviteCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;
        private readonly IInvitationService invitationService;

        public DeclineInviteCommand(
            IUserService userService,
            ITeamService teamService,
            IInvitationService invitationService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        //	DeclineInvite <teamName>
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

            var loggedInUser = this.userService.GetCurrentUser();
            var team = this.teamService.ByName(teamName);
            var invitationExists = this.invitationService.Exists(team.Id, loggedInUser.Id);
            if (!invitationExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            this.invitationService.DeclineInvtation(team.Id, loggedInUser.Id);

            return string.Format(Constants.SuccessfulMessages.SuccessfulDeclineInvite, teamName);
        }
    }
}
