namespace P12_Google
{
    using System;
    using System.Collections.Generic;

    public class Person
    {
        private string _name;
        private Company _company;
        private List<Pokemon> _pokemons;
        private List<Relative> _parents;
        private List<Relative> _children;
        private Car _car;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public List<Pokemon> Pokemons
        {
            get { return _pokemons; }
            set { _pokemons = value; }
        }

        public List<Relative> Parents
        {
            get { return _parents; }
            set { _parents = value; }
        }

        public List<Relative> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public Car Car
        {
            get { return _car; }
            set { _car = value; }
        }

        public Person(string name)
        {
            _name = name;
            _company = new Company();
            _pokemons = new List<Pokemon>();
            _parents = new List<Relative>();
            _children = new List<Relative>();
            _car = new Car();
        }

        public override string ToString()
        {
            return $"{Name}{Environment.NewLine}" +
                   $"Company:{Company}{Environment.NewLine}" +
                   $"Car:{Car}{Environment.NewLine}" +
                   $"Pokemon:{string.Join("", Pokemons)}{Environment.NewLine}" +
                   $"Parents:{string.Join("", Parents)}{Environment.NewLine}" +
                   $"Children:{string.Join("", Children)}";
        }
    }
}