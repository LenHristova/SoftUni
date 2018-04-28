using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class PickUpItemCommand : Command
    {
        private readonly IPool pool;
        private readonly IParty party;

        public PickUpItemCommand(IPool pool, IParty party)
        {
            this.pool = pool;
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var characterName = args[0];
            var character = party.FindCharacter(characterName);
            var item = pool.PickUpItem();
            character.Bag.AddItem(item);

            return $"{character.Name} picked up {item.Name}!";
        }
    }
}