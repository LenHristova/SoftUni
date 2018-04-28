using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class UseItemCommand : Command
    {
        private readonly IParty party;

        public UseItemCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var characterName = args[0];
            var character = party.FindCharacter(characterName);

            var itemName = args[1];
            var item = character.Bag.GetItem(itemName);

            character.UseItem(item);

            return $"{character.Name} used {itemName}.";
        }
    }
}