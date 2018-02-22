using System;
using System.Globalization;

namespace P05_PizzaCalories
{
    public class Ingredient
    {
        private const double BaseCalories = 2;

        private readonly string _type;
        private double _weight;
        private readonly int _maxWeight;
        protected double ModifiersCalories;

        private double Weight
        {
            set
            {
                if (value < 1 || value > _maxWeight)
                {
                    throw new ArgumentException($"{_type} weight should be in the range [1..{_maxWeight}].");
                }

                _weight = value;
            }
        }

        public double Calories => _weight * BaseCalories * ModifiersCalories;

        public Ingredient(string type, double weight)
        {
            _type = type;
            _maxWeight = _type == "Dough" ? 200 : 50;
            Weight = weight;
        }

        protected double Modify(string ingredientType, Type type)
        {
            var ingredient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ingredientType.ToLower());
            return (int)Enum.Parse(type, ingredient) / 100.0;
        }
    }
}
