namespace TeamBuilder.Services.Contracts
{
    using Models;

    public interface ITeamService
    {
        bool Exists(string teamName);

        void CreateTeam(string name, string acronym, string description, int creatorId);

        Team ByName(string teamName);

        void Disband(Team team);
    }
}
