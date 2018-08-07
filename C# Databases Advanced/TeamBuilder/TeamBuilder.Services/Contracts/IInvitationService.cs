namespace TeamBuilder.Services.Contracts
{
    public interface IInvitationService
    {
        bool Exists(int teamId, int userId);

        void AddInvitation(int teamId, int invitedUserId);

        void DeclineInvtation(int teamId, int userId);
    }
}
