using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class AttackCommand : Command
    {
        private readonly IParty party;

        public AttackCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var attackerName = args[0];
            var receiverName = args[1];

            var attacker = party.FindCharacter(attackerName);
            var receiver = party.FindCharacter(receiverName);

            if (!(attacker is IAttackable attackable))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CANT_ATTACK, attacker.Name));
            }

            attackable.Attack(receiver);

            var otput =
                $"{attacker.Name} attacks {receiver.Name} for {attacker.AbilityPoints} hit points! {receiver.Name} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!";

            if (!receiver.IsAlive)
            {
                otput += Environment.NewLine + $"{receiver.Name} is dead!";
            }

            return otput;
        }
    }
}