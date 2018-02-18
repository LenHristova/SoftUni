namespace P11_PokemonTrainer
{
    public class Pokemon
    {
        private string _name;
        private string _element;
        private int _health;
        private bool _isAlive;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Element
        {
            get => _element;
            set => _element = value;
        }

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public bool IsAlive => Health > 0;

        public Pokemon(string name, string element, int health)
        {
            _name = name;
            _element = element;
            _health = health;
        }

        public void DecreaseHealth()
        {
            Health -= 10;
        }
    }
}