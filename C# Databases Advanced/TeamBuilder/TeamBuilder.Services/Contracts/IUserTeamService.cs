namespace TeamBuilder.Services.Contracts
{
    public interface IUserTeamService
    {
        void AddUserTeam(int teamId, int userId);

        void KickMemberOfTeam(int teamId, int memberToKickId);
    }
}
