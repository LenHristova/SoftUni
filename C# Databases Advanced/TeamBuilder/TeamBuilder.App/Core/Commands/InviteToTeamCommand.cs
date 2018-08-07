namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Linq;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class InviteToTeamCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;
        private readonly IInvitationService invitationService;

        public InviteToTeamCommand
            (IUserService userService, 
            ITeamService teamService, 
            IInvitationService invitationService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        //	InviteToTeam<teamName> <username>
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var teamName = args[0];
            var userUsername = args[1];

            var teamExists = this.teamService.Exists(teamName);
            var userExists = this.userService.Exists(userUsername);

            if (!teamExists || !userExists)
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            var team = this.teamService.ByName(teamName);
            var loggedInUserId = this.userService.GetCurrentUser().Id;
            var memberToAddId = this.userService.ByUsername(userUsername).Id;

            var isLoggedInUserCreator = team.CreatorId == loggedInUserId;
            var isLoggedInUserMember = team.Members.Any(m => m.UserId == loggedInUserId);
            var isNewUserAlreadyMember = team.Members.Any(m => m.UserId == memberToAddId);

            if ((!isLoggedInUserCreator && !isLoggedInUserMember) || isNewUserAlreadyMember)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var isNewMemberInvated = this.invitationService.Exists(team.Id, memberToAddId);
            if (isNewMemberInvated)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            this.invitationService.AddInvitation(team.Id, memberToAddId);

            return string.Format(Constants.SuccessfulMessages.SuccessfulInvateToTeam, teamName, userUsername);
        }
    }
}
