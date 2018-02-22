using System;
using System.Collections.Generic;
using System.Linq;

namespace P05_PizzaCalories
{
    public class Pizza
    {
        private string _name;
        private Dough _dough;
        private readonly List<Topping> _toppings;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                _name = value;
            }
        }

        public double TotalCalories => _dough.Calories + _toppings.Select(t => t.Calories).Sum();

        public Pizza(string name)
        {
            Name = name;
            _toppings = new List<Topping>();
        }

        public void AddDough(Dough dough)
        {
            _dough = dough;
        }

        public void TryToAddTopping(Topping topping)
        {
            if (_toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }

            _toppings.Add(topping);
        }
    }
}