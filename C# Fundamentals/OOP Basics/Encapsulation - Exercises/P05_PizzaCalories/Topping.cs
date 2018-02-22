using System;

namespace P05_PizzaCalories
{
    public class Topping : Ingredient
    {
        public Topping(double weight, string toppingType):base(toppingType, weight)
        {
            ModifiersCalories = GetModifiersCalories(toppingType);
        }

        private double GetModifiersCalories(string toppingType)
        {
            try
            {
                return base.Modify(toppingType, typeof(ToppingType));
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Cannot place {toppingType} on top of your pizza.");
            }
        }

        private enum ToppingType
        {
            Meat = 120,
            Veggies = 80,
            Cheese = 110,
            Sauce = 90
        }
    }
}