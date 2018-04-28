using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Core
{
    public class Party : IParty
    {
        private readonly IList<ICharacter> characters;
        private int lastSurvivorRounds;

        public Party(IList<ICharacter> characters)
        {
            this.characters = characters;
        }

        public string JoinParty(ICharacter character)
        {
            characters.Add(character);
            return $"{character.Name} joined the party!";
        }

        public string GetStats()
        {
            var sortedCharacters = characters
                .OrderByDescending(ch => ch.IsAlive)
                .ThenByDescending(ch => ch.Health);

            var result = string.Join(Environment.NewLine, sortedCharacters);
            return result;
        }

        public string EndTurn(string[] args)
        {
            var alives = characters.Where(ch => ch.IsAlive).ToArray();

            var sb = new StringBuilder();
            foreach (var character in alives)
            {
                var healthBeforeRest = character.Health;
                character.Rest();
                var currentHealth = character.Health;
                sb.AppendLine($"{character.Name} rests ({healthBeforeRest} => {currentHealth})");
            }

            if (alives.Length < 2)
            {
                lastSurvivorRounds++;
            }

            return sb.ToString().TrimEnd();
        }

        public bool IsGameOver()
        {
            var oneOrZeroSurvivorsLeft = characters.Count(c => c.IsAlive) <= 1;
            var lastSurviverSurvivedTooLong = lastSurvivorRounds > 1;

            return oneOrZeroSurvivorsLeft && lastSurviverSurvivedTooLong;
        }

        public ICharacter FindCharacter(string characterName)
        {
            var character = characters.FirstOrDefault(ch => ch.Name == characterName);
            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CHARACTER_NOT_FOUND, characterName));
            }

            return character;
        }
    }
}

