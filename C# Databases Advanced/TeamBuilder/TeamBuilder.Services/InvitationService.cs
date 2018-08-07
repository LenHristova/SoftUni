namespace TeamBuilder.Services
{
    using System.Linq;
    using Contracts;
	using Data;
	using Models;

    public class InvitationService : IInvitationService
    {
        private readonly TeamBuilderContext context;

        public InvitationService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public bool Exists(int teamId, int userId)
            => this.GetInvitation(teamId, userId) != null;

        public void AddInvitation(int teamId, int invitedUserId)
        {
            var invitation = new Invitation
            {
                TeamId = teamId,
                InvitedUserId = invitedUserId,
            };

            this.context.Invitations.Add(invitation);
            this.context.SaveChanges();
        }

        public void DeclineInvtation(int teamId, int userId)
        {
            var invitation = this.GetInvitation(teamId, userId);
            invitation.IsActive = false;

            this.context.SaveChanges();
        }

        private Invitation GetInvitation(int teamId, int userId)
            => this.context.Invitations
                .SingleOrDefault(i => i.TeamId == teamId &&
                          i.InvitedUserId == userId &&
                          i.IsActive);
    }
}
