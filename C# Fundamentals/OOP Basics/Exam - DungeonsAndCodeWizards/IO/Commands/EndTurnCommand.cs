using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class EndTurnCommand : Command
    {
        private readonly IParty party;

        public EndTurnCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            return party.EndTurn(args);
        }
    }
}