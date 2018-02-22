using System;

namespace P05_PizzaCalories
{
    public class Dough : Ingredient
    {
        public Dough(double weight, string flourType, string bakingTechnique) : base("Dough", weight)
        {
            ModifiersCalories = GetModifiersCalories(flourType, bakingTechnique);
        }

        private double GetModifiersCalories(string flourType, string bakingTechnique)
        {
            try
            {
                var flourTypeModifier = base.Modify(flourType, typeof(FlourType));
                var bakingTechniqueModifier = base.Modify(bakingTechnique, typeof(BakingTechnique));

                return flourTypeModifier * bakingTechniqueModifier;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Invalid type of dough.");
            }
        }

        private enum FlourType
        {
            White = 150,
            Wholegrain = 100,
            Crispy = 90
        }

        private enum BakingTechnique
        {
            Crispy = 90,
            Chewy = 110,
            Homemade = 100
        }
    }
}