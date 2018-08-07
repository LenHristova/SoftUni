namespace TeamBuilder.App.Core.Commands
{
	using System;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class CreateTeamCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;

        public CreateTeamCommand(IUserService userService, ITeamService teamService)
        {
            this.userService = userService;
            this.teamService = teamService;
        }

        //	CreateTeam <name> <acronym> [<description>]
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var name = args[0];
            ValidateName(name);

            var acronym = args[1];
            ValidateAcronym(acronym);

            string description = null;
            if (args.Length == 3)
            {
                description = args[2];
                ValidateDescription(description);
            }

            var teamExists = this.teamService.Exists(name);
            if (teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, name));
            }

            this.teamService.CreateTeam(name, acronym, description, userService.GetCurrentUser().Id);

            return string.Format(Constants.SuccessfulMessages.SuccessfulCreatedTeam, name);
        }

        private static void ValidateAcronym(string acronym)
        {
            var isValid = acronym.Length == Constants.TeamAcronymLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamAcronymNotValid, acronym));
            }
        }

        private static void ValidateDescription(string description)
        {
            var isValid = description.Length <= Constants.MaxTeamDescriptionLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamDescriptionNotValid, description));
            }
        }

        private static void ValidateName(string name)
        {
            var isValid = name.Length <= Constants.MaxTeamNameLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNameNotValid, name));
            }
        }
    }
}
