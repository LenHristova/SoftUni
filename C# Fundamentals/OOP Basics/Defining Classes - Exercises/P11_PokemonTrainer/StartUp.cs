namespace P11_PokemonTrainer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var trainers = new Dictionary<string, Trainer>();

            //Get all trainers
            string input;
            while ((input = Console.ReadLine()) != "Tournament")
            {
                if (input == null) continue;
                var info = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                var trainerName = info[0];
                var pokemonName = info[1];
                var pokemonElement = info[2];
                var pokemonHealth = int.Parse(info[3]);

                if (!trainers.ContainsKey(trainerName))
                {
                    trainers.Add(trainerName, new Trainer(trainerName));
                }

                var currentPokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);
                AddPokemonByElement(trainers[trainerName], currentPokemon);
            }

            //Execute command
            //Check if every trainer has pokemnon from given element 
            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                var element = command;
                foreach (var trainer in trainers.Values)
                {
                    //If has such pokemon -> earn badge
                    if (HasPokemonFromThisElement(element, trainer))
                    {
                        trainer.BadgesCount++;
                    }
                    //else all his pokemons loses 10 health
                    else
                    {
                        trainer.DecreasePokemonsHealth();
                    }
                }
            }

            //Print trainer by count of them badges descending
            foreach (var trainer in trainers.Values.OrderByDescending(t => t.BadgesCount))
            {
                Console.WriteLine(trainer);
            }
        }

        //Check by count of pokemons in group of given element
        private static bool HasPokemonFromThisElement(string element, Trainer trainer)
        {
            switch (element.ToLower())
            {
                case "fire":
                    if (trainer.FirePokemons.Count > 0)
                        return true;
                    break;
                case "water":
                    if (trainer.WaterPokemons.Count > 0)
                        return true;
                    break;
                case "electricity":
                    if (trainer.ElectricityPokemons.Count > 0)
                        return true;
                    break;
            }

            return false;
        }

        //Add pokemon in group he belong by element
        private static void AddPokemonByElement(Trainer trainer, Pokemon pokemon)
        {
            switch (pokemon.Element.ToLower())
            {
                case "fire":
                    trainer.FirePokemons.Add(pokemon);
                    break;
                case "water":
                    trainer.WaterPokemons.Add(pokemon);
                    break;
                case "electricity":
                    trainer.ElectricityPokemons.Add(pokemon);
                    break;
                default:
                    trainer.OtherPokemons.Add(pokemon);
                    break;
            }
        }
    }
}