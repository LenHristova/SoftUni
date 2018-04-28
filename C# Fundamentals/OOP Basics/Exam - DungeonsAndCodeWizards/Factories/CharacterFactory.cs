using System;
using System.Linq;
using System.Reflection;

using DungeonsAndCodeWizards.Contracts;
using DungeonsAndCodeWizards.Models.Characters;

namespace DungeonsAndCodeWizards.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        public ICharacter CreateCharacter(string faction, string type, string name)
        {
            var isValidFaction = Enum.TryParse<Faction>(faction, out var parsedFaction);

            if (!isValidFaction)
            {
                throw new ArgumentException($"Invalid faction \"{ faction }\"!");
            }

            var characterType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            if (characterType == null)
            {
                throw new ArgumentException("Character not found");
            }

            if (!typeof(ICharacter).IsAssignableFrom(characterType))
            {
                throw new InvalidOperationException($"{type} is not ICharacter");
            }

            var character = (ICharacter)Activator.CreateInstance(characterType, name, parsedFaction);

            return character;
        }
    }
}
