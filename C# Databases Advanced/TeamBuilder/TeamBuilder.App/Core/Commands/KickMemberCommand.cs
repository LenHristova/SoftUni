namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class KickMemberCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;
        private readonly IUserTeamService userTeamService;

        public KickMemberCommand(IUserService userService, ITeamService teamService, IUserTeamService userTeamService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.userTeamService = userTeamService;
        }

        //	KickMember <teamName> <username>
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

            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var userExists = this.userService.Exists(userUsername);
            if (!userExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, userUsername));
            }

            var team = this.teamService.ByName(teamName);
            var loggedInUserId = this.userService.GetCurrentUser().Id;

            var isLoggedInUserCreator = team.CreatorId == loggedInUserId;

            if (!isLoggedInUserCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var memberToKickId = this.userService.ByUsername(userUsername).Id;
            var isMemberToKickInTeam = team.Members.Any(m => m.UserId == memberToKickId);
            if (!isMemberToKickInTeam)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, userUsername, teamName));
            }

            var isMemberToKickCreator = team.CreatorId == memberToKickId;
            if (isMemberToKickCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CommandNotAllowed);
            }

            this.userTeamService.KickMemberOfTeam(team.Id, memberToKickId);

            return string.Format(Constants.SuccessfulMessages.SuccessfulKickMember, userUsername, teamName);
        }
    }
}
