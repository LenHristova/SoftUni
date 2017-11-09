using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonEvolution
{
    class Evolutions
    {
        public string Type { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return $"{Type} <-> {Index}";
        }
    }

    class StartUp
    {
        static void Main()
        {
            Dictionary<string, List<Evolutions>> pokemonsEvolutions = new Dictionary<string, List<Evolutions>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "wubbalubbadubdub")
                    break;

                string[] pokemonInfo = input
                    .Split(new[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);

                string pokemonName = pokemonInfo[0];
                if (pokemonInfo.Length==1)
                {
                    if (pokemonsEvolutions.ContainsKey(pokemonName))
                    {
                        PrintPokemonEvolutions(pokemonName, pokemonsEvolutions);
                    }
                }
                else
                {
                    string pokemonType = pokemonInfo[1];
                    int pokemonIndex = int.Parse(pokemonInfo[2]);
                    AddPokemonEvolutions(pokemonName, pokemonType, pokemonIndex, pokemonsEvolutions);
                }
            }

            PrintPokemons(pokemonsEvolutions);
        }

        private static void PrintPokemons(Dictionary<string, List<Evolutions>> pokemonsEvolutions)
        {
            foreach (var pokemon in pokemonsEvolutions)
            {
                Console.WriteLine($"# {pokemon.Key}");
                foreach (var evolution in pokemon.Value.OrderByDescending(p => p.Index))
                {
                    Console.WriteLine(evolution);
                }
            }
        }

        private static void AddPokemonEvolutions(string pokemonName, string pokemonType, int pokemonIndex, Dictionary<string, List<Evolutions>> pokemonsEvolutions)
        {
            if (!pokemonsEvolutions.ContainsKey(pokemonName))
            {
                pokemonsEvolutions[pokemonName] = new List<Evolutions>();
            }
            pokemonsEvolutions[pokemonName].Add(new Evolutions()
            {
                Type = pokemonType,
                Index = pokemonIndex
            });
        }

        private static void PrintPokemonEvolutions(string pokemonName, Dictionary<string, List<Evolutions>> pokemonsEvolutions)
        {
            var serchedPokemon = pokemonsEvolutions
                .First(p => p.Key == pokemonName);

            Console.WriteLine($"# {serchedPokemon.Key}");
            Console.WriteLine(string.Join(Environment.NewLine, serchedPokemon.Value));
        }
    }
}
