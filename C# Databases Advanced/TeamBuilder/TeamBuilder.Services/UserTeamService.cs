namespace TeamBuilder.Services
{
    using System.Linq;
    using Contracts;
	using Data;
	using Models;

    public class UserTeamService : IUserTeamService
    {
        private readonly TeamBuilderContext context;

        public UserTeamService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public void AddUserTeam(int teamId, int userId)
        {
            var userTeam = new UserTeam
            {
                TeamId = teamId,
                UserId = userId
            };

            this.context.UserTeams.Add(userTeam);
            this.context.SaveChanges();
        }

        public void KickMemberOfTeam(int teamId, int memberToKickId)
        {
            var userTeamToDelete = this.context.UserTeams
                .Single(ut => ut.TeamId == teamId && ut.UserId == memberToKickId);

            this.context.UserTeams.Remove(userTeamToDelete);
            this.context.SaveChanges();
        }
    }
}
