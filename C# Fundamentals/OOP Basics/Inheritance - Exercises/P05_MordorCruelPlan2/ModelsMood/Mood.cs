using System;

namespace P05_MordorCruelPlan2.ModelsMood
{
    public abstract class Mood
    {
        public string Name { get; set; }
        public int HappinessPoints { get; set; }

        protected Mood(string name, int happinessPoints)
        {
            Name = name;
            HappinessPoints = happinessPoints;
        }

        public override string ToString()
        {
            return $"{HappinessPoints}{Environment.NewLine}" +
                   $"{Name}";
        }
    }
}