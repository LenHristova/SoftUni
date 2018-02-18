namespace P11_PokemonTrainer
{
    using System.Collections.Generic;
    using System.Linq;

    public class Trainer
    {
        private string _name;
        private int _badgesCount;
        private List<Pokemon> _firePokemons;
        private List<Pokemon> _waterPokemons;
        private List<Pokemon> _electricityPokemons;
        private List<Pokemon> _otherPokemons;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int BadgesCount
        {
            get => _badgesCount;
            set => _badgesCount = value;
        }

        public List<Pokemon> FirePokemons
        {
            get => _firePokemons;
            set => _firePokemons = value;
        }

        public List<Pokemon> WaterPokemons
        {
            get => _waterPokemons;
            set => _waterPokemons = value;
        }

        public List<Pokemon> ElectricityPokemons
        {
            get => _electricityPokemons;
            set => _electricityPokemons = value;
        }

        public List<Pokemon> OtherPokemons
        {
            get => _otherPokemons;
            set => _otherPokemons = value;
        }

        public Trainer(string name)
        {
            _name = name;
            _badgesCount = 0;
            _firePokemons = new List<Pokemon>();
            _waterPokemons = new List<Pokemon>();
            _electricityPokemons = new List<Pokemon>();
            _otherPokemons = new List<Pokemon>();
        }

        public override string ToString()
        {
            var allPokemosCount = FirePokemons.Count + WaterPokemons.Count + ElectricityPokemons.Count + OtherPokemons.Count;
            return $"{Name} {BadgesCount} {allPokemosCount}";
        }

        public void DecreasePokemonsHealth()
        {
            FirePokemons = DecreasePokemonsHealth(FirePokemons);
            WaterPokemons = DecreasePokemonsHealth(WaterPokemons);
            ElectricityPokemons = DecreasePokemonsHealth(ElectricityPokemons);
            OtherPokemons = DecreasePokemonsHealth(OtherPokemons);
        }

        private List<Pokemon> DecreasePokemonsHealth(List<Pokemon> pokemons)
        {
            var hasDeadPokemons = false;
            foreach (var pokemon in pokemons)
            {
                pokemon.DecreaseHealth();
                if (!pokemon.IsAlive)
                {
                    hasDeadPokemons = true;
                }
            }

            // If has dead pokemons (health <= 0) -> remove from list
            if (hasDeadPokemons)
            {
                pokemons = pokemons
                    .Where(p => p.IsAlive)
                    .ToList();
            }

            return pokemons;
        }
    }
}