namespace TeamBuilder.Services
{
    using System.Linq;
    using Contracts;
	using Data;
	using Models;

    public class TeamService : ITeamService
    {
        private readonly TeamBuilderContext context;

        public TeamService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public bool Exists(string teamName)
            => this.ByName(teamName) != null;

        public Team ByName(string teamName)
            => this.context.Teams
                   .SingleOrDefault(t => t.Name == teamName);

        public void Disband(Team team)
        {
            this.context.Remove(team);
            this.context.SaveChanges();
        }

        public void CreateTeam(string name, string acronym, string description, int creatorId)
        {
            var team = new Team
            {
                Name = name,
                Acronym = acronym,
                Description = description,
                CreatorId = creatorId
            };

            this.context.Teams.Add(team);
            this.context.SaveChanges();
        }
    }
}
