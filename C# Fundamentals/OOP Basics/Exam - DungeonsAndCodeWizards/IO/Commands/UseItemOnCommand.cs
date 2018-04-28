using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class UseItemOnCommand : Command
    {
        private readonly IParty party;

        public UseItemOnCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var giverName = args[0];
            var receiverName = args[1];
            var itemName = args[2];

            var giver = party.FindCharacter(giverName);
            var receiver = party.FindCharacter(receiverName);
            var item = giver.Bag.GetItem(itemName);

            giver.UseItemOn(item, receiver);

            return $"{giver.Name} used {itemName} on {receiver.Name}.";
        }
    }
}