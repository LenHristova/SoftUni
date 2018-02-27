namespace P05_MordorCruelPlan2.ModelsFood
{
    public abstract class Food
    {
        public int HappinessPoints { get; set; }

        protected Food(int happinessPoints)
        {
            HappinessPoints = happinessPoints;
        }
    }
}