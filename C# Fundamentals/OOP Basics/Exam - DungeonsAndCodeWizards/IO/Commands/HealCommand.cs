using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO.Commands
{
    public class HealCommand:Command
    {
        private readonly IParty party;

        public HealCommand(IParty party)
        {
            this.party = party;
        }

        public override string Execute(params string[] args)
        {
            var healerName = args[0];
            var healingReceiverName = args[1];

            var healer = party.FindCharacter(healerName);
            var receiver = party.FindCharacter(healingReceiverName);

            if (!(healer is IHealable healable))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CANT_HEAL, healerName));
            }

            healable.Heal(receiver);

            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
        }
    }
}