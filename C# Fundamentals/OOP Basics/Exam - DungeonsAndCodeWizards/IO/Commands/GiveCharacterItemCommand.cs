using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class GiveCharacterItemCommand : Command
    {
        private readonly IParty party;

        public GiveCharacterItemCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var giverName = args[0];
            var receiverName = args[1];
            var itemName = args[2];

            var giver = party.FindCharacter(giverName);
            var item = giver.Bag.GetItem(itemName);

            var receiver = party.FindCharacter(receiverName);
            giver.GiveCharacterItem(item, receiver);

            return $"{giver.Name} gave {receiver.Name} {itemName}.";
        }
    }
}