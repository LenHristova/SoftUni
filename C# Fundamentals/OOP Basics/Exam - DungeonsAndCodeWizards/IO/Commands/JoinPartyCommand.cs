using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class JoinPartyCommand : Command
    {
        private readonly ICharacterFactory characterFactory;
        private readonly IParty party;

        public JoinPartyCommand(ICharacterFactory characterFactory, IParty party)
        {
            this.characterFactory = characterFactory;
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var factionType = args[0];
            var characterType = args[1];
            var name = args[2];
            var character = characterFactory.CreateCharacter(factionType, characterType, name);

            return party.JoinParty(character);
        }
    }
}