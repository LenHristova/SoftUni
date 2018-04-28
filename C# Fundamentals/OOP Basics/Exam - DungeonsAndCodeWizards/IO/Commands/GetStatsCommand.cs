using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class GetStatsCommand:Command
    {
        private readonly IParty party;

        public GetStatsCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            return party.GetStats();
        }
    }
}